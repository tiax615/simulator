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
        public float Frequency = 12.5f; // 更新频率

        [SensorParameter]
        public bool IgnoreMapOrigin = false;

        Queue<Tuple<double, Action>> MessageQueue =
            new Queue<Tuple<double, Action>>(); // 定义了一个队列，用于储存GPS数据

        bool Destroyed = false;
        bool IsFirstFixedUpdate = true; // 是否第一次执行FirstFixedUpdate
        double LastTimestamp; // 上一次执行Publish()的时间戳

        float NextSend; // 没有用到
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
            var nextPublish = Stopwatch.GetTimestamp(); // 初始化nextPublish为当前时间戳

            while (!Destroyed)
            {
                long now = Stopwatch.GetTimestamp(); // 获取当前时间戳
                if (now < nextPublish) // 当前时间小于nextPublish时，continue
                {
                    Thread.Sleep(0);
                    continue;
                }

                Tuple<double, Action> msg = null; // 定义一个空的2元组
                lock (MessageQueue)
                {
                    if (MessageQueue.Count > 0)
                    {
                        msg = MessageQueue.Dequeue(); // 如果MessageQueue中有元素，取出开头的数据
                    }
                }

                if (msg != null)
                {
                    try
                    {
                        msg.Item2(); // msg不为空时，获取元组中第二个分量的值（是个Action）
                    }
                    catch
                    {
                    }
                    nextPublish = now + (long)(Stopwatch.Frequency / Frequency); // 将nextPublish加一个频率的时间
                    LastTimestamp = msg.Item1; // 将Publish()的时间存在LastTimestamp中
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
                    MessageQueue.Clear(); // 如果是第一次执行这里，清空队列MessageQueue
                }
                IsFirstFixedUpdate = false; // 如果是第一次执行这里，将IsFirstFixedUpdate设置为false
            }

            var time = SimulatorManager.Instance.CurrentTime; // 从SimulatorManager单例获取当前时间
            if (time < LastTimestamp) // 如果当前时间没到LastTimestamp，就return
            {
                return;
            }

            // 获取GPS数据
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
                // 创建一个Tuple，添加到MessageQueue的末尾
                // Item1是time，当前时间
                // Item2是(Action)(() => Writer.Write(data))，一个往Writer里写数据的data
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
