/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using Simulator.Bridge;
using Simulator.Bridge.Data;
using Simulator.Map;
using Simulator.Utilities;
using UnityEngine;
using Simulator.Sensors.UI;
using System.Collections.Generic;

namespace Simulator.Sensors
{
    [SensorType("CAN-Bus", new[] { typeof(CanBusData) })]
    public partial class CanBusSensor : SensorBase
    {
        [SensorParameter]
        [Range(1f, 100f)]
        public float Frequency = 10.0f;

        uint SendSequence;
        float NextSend; // 下次更新的时间

        IBridge Bridge;
        IWriter<CanBusData> Writer;

        Rigidbody RigidBody;
        IVehicleDynamics Dynamics;
        VehicleActions Actions;
        MapOrigin MapOrigin;

        CanBusData msg;
        
        public override SensorDistributionType DistributionType => SensorDistributionType.LowLoad;

        private void Awake()
        {
            RigidBody = GetComponentInParent<Rigidbody>(); // 父物体（Lincoln2017MKZ (Apollo 5.0)）的刚体组件
            Actions = GetComponentInParent<VehicleActions>(); // 父物体上的VehicleActions.cs
            Dynamics = GetComponentInParent<IVehicleDynamics>(); // 父物体上的IVehicleDynamics.cs（接口）
            MapOrigin = MapOrigin.Find(); // 找到地图原点
        }

        public override void OnBridgeSetup(IBridge bridge)
        {
            Bridge = bridge;
            Writer = bridge.AddWriter<CanBusData>(Topic);
        }

        public void Start()
        {
            NextSend = Time.time + 1.0f / Frequency; // 根据频率计算第一次NextSend时间
        }

        public void Update()
        {
            if (MapOrigin == null)
            {
                return;
            }

            if (Time.time < NextSend) // 当前时间没到NextSend时，return
            {
                return;
            }
            NextSend = Time.time + 1.0f / Frequency; // 下次的更新时间NextSend

            float speed = RigidBody.velocity.magnitude; // 获取自车刚体速度的模

            var gps = MapOrigin.GetGpsLocation(transform.position); // 获取gps信息，类型是结构体GpsLocation

            msg = new CanBusData() // 构造CanBusData类
            {
                Name = Name, // "CAN Bus"
                Frame = Frame, // ""
                Time = SimulatorManager.Instance.CurrentTime, // SimulatorManager单例的CurrentTime
                Sequence = SendSequence++, // 序列序号++

                Speed = speed,

                Throttle = Dynamics.AccellInput > 0 ? Dynamics.AccellInput : 0, // VehicleSMI中的AccellInput>0时，赋值给油门值Throttle
                Braking = Dynamics.AccellInput < 0 ? -Dynamics.AccellInput : 0, // VehicleSMI中的AccellInput<0时，赋值给刹车值Braking
                Steering = Dynamics.SteerInput,

                ParkingBrake = Dynamics.HandBrake,
                HighBeamSignal = Actions.CurrentHeadLightState == VehicleActions.HeadLightState.HIGH,
                LowBeamSignal = Actions.CurrentHeadLightState == VehicleActions.HeadLightState.LOW,
                HazardLights = Actions.HazardLights,
                FogLights = Actions.FogLights,

                LeftTurnSignal = Actions.LeftTurnSignal,
                RightTurnSignal = Actions.RightTurnSignal,

                Wipers = false,

                InReverse = Dynamics.Reverse,
                Gear = Mathf.RoundToInt(Dynamics.CurrentGear),

                EngineOn = Dynamics.CurrentIgnitionStatus == IgnitionStatus.On,
                EngineRPM = Dynamics.CurrentRPM,

                Latitude = gps.Latitude,
                Longitude = gps.Longitude,
                Altitude = gps.Altitude,

                Orientation = transform.rotation, // 传感器自身的四元数
                Velocity = RigidBody.velocity,
            };

            // 如果Bridge不为空，且状态是连接时，将msg写入Writer
            if (Bridge != null && Bridge.Status == Status.Connected)
            {
                Writer.Write(msg, null);
            }
        }

        public override void OnVisualize(Visualizer visualizer)
        {
            Debug.Assert(visualizer != null);

            if (msg == null)
            {
                return;
            }

            // 如果msg不为空，将msg的内容存在字典graphData中
            var graphData = new Dictionary<string, object>()
            {
                {"Speed", msg.Speed},
                {"Throttle", msg.Throttle},
                {"Braking", msg.Braking},
                {"Steering", msg.Steering},
                {"Parking Brake", msg.ParkingBrake},
                {"Low Beam Signal", msg.LowBeamSignal},
                {"Hazard Lights", msg.HazardLights},
                {"Fog Lights", msg.FogLights},
                {"Left Turn Signal", msg.LeftTurnSignal},
                {"Right Turn Signal", msg.RightTurnSignal},
                {"Wipers", msg.Wipers},
                {"In Reverse", msg.InReverse},
                {"Gear", msg.Gear},
                {"Engine On", msg.EngineOn},
                {"Engine RPM", msg.EngineRPM},
                {"Latitude", msg.Latitude},
                {"Longitude", msg.Longitude},
                {"Altitude", msg.Altitude},
                {"Orientation", msg.Orientation},
                {"Velocity", msg.Velocity},
            };

            // 将graphData更新在界面上
            visualizer.UpdateGraphValues(graphData);
        }

        public override void OnVisualizeToggle(bool state)
        {
            //
        }
    }
}
