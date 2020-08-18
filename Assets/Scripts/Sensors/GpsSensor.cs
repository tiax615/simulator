/**
 * Copyright (c) 2018 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using Simulator.Bridge;
using Simulator.Bridge.Data;
using Simulator.Map;
using Simulator.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Simulator.Sensors.UI;

namespace Simulator.Sensors
{
    [SensorType("GPS Device", new[] { typeof(GpsData) })]
    public class GpsSensor : SensorBase
    {
        [SensorParameter]
        [Range(1f, 100f)]
        public float Frequency = 12.5f; // ����Ƶ��

        [SensorParameter]
        public bool IgnoreMapOrigin = false;

        Queue<Tuple<double, Action>> MessageQueue =
            new Queue<Tuple<double, Action>>(); // ������һ�����У����ڴ���GPS����

        bool Destroyed = false;
        bool IsFirstFixedUpdate = true; // �Ƿ��һ��ִ��FirstFixedUpdate
        double LastTimestamp; // ��һ��ִ��Publish()��ʱ���

        float NextSend; // û���õ�
        uint SendSequence;

        IBridge Bridge;
        IWriter<GpsData> Writer;

        MapOrigin MapOrigin;
        
        public override SensorDistributionType DistributionType => SensorDistributionType.LowLoad;

        private void Awake()
        {
            MapOrigin = MapOrigin.Find();
        }

        public void Start()
        {
            Task.Run(Publisher);
        }

        void OnDestroy()
        {
            Destroyed = true;
        }

        public override void OnBridgeSetup(IBridge bridge)
        {
            Bridge = bridge;
            Writer = Bridge.AddWriter<GpsData>(Topic);
        }

        void Publisher()
        {
            var nextPublish = Stopwatch.GetTimestamp(); // ��ʼ��nextPublishΪ��ǰʱ���

            while (!Destroyed)
            {
                long now = Stopwatch.GetTimestamp(); // ��ȡ��ǰʱ���
                if (now < nextPublish) // ��ǰʱ��С��nextPublishʱ��continue
                {
                    Thread.Sleep(0);
                    continue;
                }

                Tuple<double, Action> msg = null; // ����һ���յ�2Ԫ��
                lock (MessageQueue)
                {
                    if (MessageQueue.Count > 0)
                    {
                        msg = MessageQueue.Dequeue(); // ���MessageQueue����Ԫ�أ�ȡ����ͷ������
                    }
                }

                if (msg != null)
                {
                    try
                    {
                        msg.Item2(); // msg��Ϊ��ʱ����ȡԪ���еڶ���������ֵ���Ǹ�Action��
                    }
                    catch
                    {
                    }
                    nextPublish = now + (long)(Stopwatch.Frequency / Frequency); // ��nextPublish��һ��Ƶ�ʵ�ʱ��
                    LastTimestamp = msg.Item1; // ��Publish()��ʱ�����LastTimestamp��
                }
            }
        }

        void FixedUpdate()
        {
            if (MapOrigin == null || Bridge == null || Bridge.Status != Status.Connected)
            {
                return;
            }

            if (IsFirstFixedUpdate)
            {
                lock (MessageQueue)
                {
                    MessageQueue.Clear(); // ����ǵ�һ��ִ�������ն���MessageQueue
                }
                IsFirstFixedUpdate = false; // ����ǵ�һ��ִ�������IsFirstFixedUpdate����Ϊfalse
            }

            var time = SimulatorManager.Instance.CurrentTime; // ��SimulatorManager������ȡ��ǰʱ��
            if (time < LastTimestamp) // �����ǰʱ��û��LastTimestamp����return
            {
                return;
            }

            // ��ȡGPS����
            var location = MapOrigin.GetGpsLocation(transform.position, IgnoreMapOrigin);

            var data = new GpsData()
            {
                Name = Name,
                Frame = Frame,
                Time = SimulatorManager.Instance.CurrentTime,
                Sequence = SendSequence++,

                IgnoreMapOrigin = IgnoreMapOrigin,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Altitude = location.Altitude,
                Northing = location.Northing,
                Easting = location.Easting,
                Orientation = transform.rotation,
            };
            
            lock (MessageQueue)
            {
                // ����һ��Tuple����ӵ�MessageQueue��ĩβ
                // Item1��time����ǰʱ��
                // Item2��(Action)(() => Writer.Write(data))��һ����Writer��д���ݵ�data
                MessageQueue.Enqueue(Tuple.Create(time, (Action)(() => Writer.Write(data))));
            }
        }

        void Update()
        {
            IsFirstFixedUpdate = true;
        }

        public Api.Commands.GpsData GetData()
        {
            var location = MapOrigin.GetGpsLocation(transform.position);

            var data = new Api.Commands.GpsData
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Easting = location.Easting,
                Northing = location.Northing,
                Altitude = location.Altitude,
                Orientation = -transform.rotation.eulerAngles.y
            };
            return data;
        }

        public override void OnVisualize(Visualizer visualizer)
        {
            UnityEngine.Debug.Assert(visualizer != null);

            var location = MapOrigin.GetGpsLocation(transform.position, IgnoreMapOrigin);

            var graphData = new Dictionary<string, object>()
            {
                {"Ignore MapOrigin", IgnoreMapOrigin},
                {"Latitude", location.Latitude},
                {"Longitude", location.Longitude},
                {"Altitude", location.Altitude},
                {"Northing", location.Northing},
                {"Easting", location.Easting},
                {"Orientation", transform.rotation}
            };
            visualizer.UpdateGraphValues(graphData);
        }

        public override void OnVisualizeToggle(bool state)
        {
            //
        }
    }
}
