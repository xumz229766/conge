using System;
using CMotion.Interfaces;
using CMotion.Interfaces.Axis;
using CMotion.Interfaces.Configuration;
using System.Collections.Generic;
namespace CMotion.Applications
{
    /// <summary>
    /// 轴,修改于2018.7.10 finley jiang
    /// </summary>
    public class ApsAxis : Axis, INeedClean
    {
        protected readonly IApsController ApsController;
        public ApsAxis(IApsController apsController)
        {
            ApsController = apsController;
        }

        #region Overrides of Axis

        /// <summary>
        ///     当前 Absolute 位置。
        /// </summary>
        public override double CurrentPos { get { return 0; } }
        public override double CurrentSpeed
        {
            get
            {
                return Convert.ToDouble(ApsController.GetCurrentCommandSpeed(NoId)) / Transmission.EquivalentPulse;
            }
        }
        /// <summary>
        /// 轴传动参数
        /// </summary>
        public TransmissionParams Transmission { get; set; }

        #region 获取当前轴的 IO 信号
        /// <summary>
        ///     是否已励磁。
        /// </summary>
        public bool IsServon
        {
            get { return ApsController.GetServo(NoId); }
            set
            {
                if (value)
                {
                    ApsController.ServoOn(NoId);
                }
                else
                {
                    ApsController.ServoOff(NoId);
                }
            }
        }
        public uint HomeMode { get; set; }
        public uint HomeDir { get; set; }
        /// <summary>
        ///     是否到达正限位
        /// </summary>
        /// <returns></returns>
        public bool IsPEL
        {
            get { return ApsController.IsPel(NoId); }
        }
        /// <summary>
        ///     是否到达正负位
        /// </summary>
        /// <returns></returns>
        public bool IsMEL
        {
            get { return ApsController.IsMel(NoId); }
        }

        /// <summary>
        ///     是否在轴原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsOrign
        {
            get { return ApsController.IsOrg(NoId); }
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsSZ
        {
            get { return ApsController.IsSZ(NoId); }
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsINP
        {
            get { return ApsController.IsINP(NoId); }
        }
        /// <summary>
        /// 外部信号
        /// </summary>
        public External External { get; set; }
        public override bool IsAlarmed { get{return false;} }

        public override bool IsEmg { get { return false; } }
        #endregion
        /// <summary>
        ///     是否已完成最后运动指令。
        /// </summary>
        /// <code>? + var isReach = Math.Abs(commandPosition - currentPosition) &lt; Precision;</code>
        public override bool IsDone
        {
            get { return ApsController.IsDown(NoId); }
        }

        /// <summary>
        /// 运动轴轴移动到指定的位置。
        /// </summary>
        /// <param name="value">将要移动到的位置。</param>
        /// <param name="velocityCurve">移动时的运行参数。</param>
        public override void MoveTo(double value, VelocityCurve velocityCurve = null)
        {
            var Data = value * Transmission.EquivalentPulse;
            var velocity = new VelocityCurve()
            {
                Strvel = velocityCurve.Strvel * Transmission.EquivalentPulse,
                Maxvel = velocityCurve.Maxvel * Transmission.EquivalentPulse,
                Tacc = velocityCurve.Tacc,
                Tdec = velocityCurve.Tdec,
                VelocityCurveType = velocityCurve.VelocityCurveType
            };
            ApsController.MoveAbsPulse(NoId, (int)Data, velocity);
        }

        /// <summary>
        ///     运动轴相对移动到指定位置。
        /// </summary>
        /// <param name="value">要移动到的距离。</param>
        /// <param name="velocityCurve"></param>
        public override void MoveDelta(double value, VelocityCurve velocityCurve = null)
        {
            var Data = value * Transmission.EquivalentPulse;
            var velocity = new VelocityCurve()
            {
                Strvel = velocityCurve.Strvel * Transmission.EquivalentPulse,
                Maxvel = velocityCurve.Maxvel * Transmission.EquivalentPulse,
                Tacc = velocityCurve.Tacc,
                Tdec = velocityCurve.Tdec,
                VelocityCurveType = velocityCurve.VelocityCurveType
            };
            ApsController.MoveRelPulse(NoId, (int)Data, velocity);
        }

        /// <summary>
        ///     正向移动。
        /// </summary>
        public override void Postive()
        {
            var velocityCurve = new VelocityCurve
            {
                Strvel = 0,
                Maxvel = (Speed ?? 20) * Transmission.EquivalentPulse,
                Tacc = 0.1,
                Tdec = 0.1,
                VelocityCurveType = CurveTypes.T
            };
            ApsController.ContinuousMove(NoId, MoveDirection.Postive, velocityCurve);
        }

        /// <summary>
        ///     反向移动。
        /// </summary>
        public override void Negative()
        {
            var velocityCurve = new VelocityCurve
            {
                Strvel = 0,
                Maxvel = (Speed ?? 20) * Transmission.EquivalentPulse,
                Tacc = 0.1,
                Tdec = 0.1,
                VelocityCurveType = CurveTypes.T
            };
            ApsController.ContinuousMove(NoId, MoveDirection.Negative, velocityCurve);
        }

        /// <summary>
        ///     轴停止运动。
        /// </summary>
        /// <param name="velocityCurve"></param>
        public override void Stop(VelocityCurve velocityCurve = null)
        {
            ApsController.DecelStop(NoId);
        }

        public override void Initialize()
        {
            //ApsController.MoveOrigin(NoId);
        }

        #endregion

        #region Implementation of INeedInitialization

        #endregion

        #region Implementation of INeedClean

        /// <summary>
        ///      清除
        /// </summary>
        public void Clean()
        {
            //Stop();
            ApsController.CleanError(NoId);
        }

        public override bool IsInPosition(double pos)
        {
            throw new NotImplementedException();
        }

        public override void BackHome(HomeParams homeParams = null)
        {
            var velocity = new HomeParams() { VelocityCurve = new VelocityCurve()};
            velocity.VelocityCurve.Strvel = homeParams.VelocityCurve.Strvel * Transmission.EquivalentPulse;
            velocity.VelocityCurve.Maxvel = homeParams.VelocityCurve.Maxvel * Transmission.EquivalentPulse;
            velocity.VelocityCurve.Tacc = homeParams.VelocityCurve.Tacc;
            velocity.VelocityCurve.Tdec = homeParams.VelocityCurve.Tdec;
            velocity.VelocityCurve.Sfac = homeParams.VelocityCurve.Sfac;
            velocity.VelocityCurve.Svacc = homeParams.VelocityCurve.Svacc;
            velocity.VelocityCurve.Svdec = homeParams.VelocityCurve.Svdec;
            velocity.VelocityCurve.VelocityCurveType = homeParams.VelocityCurve.VelocityCurveType;
            velocity.Mode = homeParams.Mode;
            velocity.Dir = homeParams.Dir;
            velocity.EZ = homeParams.EZ;
            velocity.Timeout = homeParams.Timeout;
            velocity.ZeroOffset = homeParams.ZeroOffset;
            ApsController.BackHome(NoId, velocity);
        }
        public override int CheckHomeDone(double timeoutLimit) { return ApsController.CheckHomeDone(NoId, timeoutLimit); }

        public override void APS_set_command(double pos)
        {
            throw new NotImplementedException();
        }

        public override StopReasons GetStopReasons
        {
            get
            {
                return (StopReasons)0;
            }
        }
        /// <summary>
        /// 轴报警集合
        /// </summary>
        public IList<Alarm> Alarms
        {
            get
            {
                var list = new List<Alarm>();
                list.Add(new Alarm(() => ApsController.IsAlm(NoId)) { External = External, AlarmLevel = AlarmLevels.Error, Name = Name + "故障报警" });
                list.Add(new Alarm(() => ApsController.GetState(NoId)==0) { External = External, AlarmLevel = AlarmLevels.Error, Name = Name + "被禁用" });
                list.Add(new Alarm(() => ApsController.GetState(NoId) == 3) { External = External, AlarmLevel = AlarmLevels.Error, Name = Name + "出错,轴停止" });
                return list;
            }
        }
        #endregion
    }
}