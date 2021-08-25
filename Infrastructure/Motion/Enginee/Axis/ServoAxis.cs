using CMotion.Interfaces;
using System;
namespace CMotion.Applications
{
    /// <summary>
    /// 伺服马达驱动轴
    /// </summary>
    public class ServoAxis : ApsAxis
    {
        public ServoAxis(IApsController apsController) : base(apsController)
        {
        }
        public override double CurrentPos
        {
            get
            {
                var Data= ApsController.GetCurrentFeedbackPosition(NoId) * Transmission.PulseEquivalent;
                return Math.Round(Data, 3);
            }
        }
        /// <summary>
        ///     是否原点
        /// </summary>
        public bool IsOrigin
        {
            get { return ApsController.IsOrg(NoId); }
        }
        /// <summary>
        ///     是否报警
        /// </summary>
        public override bool IsAlarmed
        {
            get { return ApsController.IsAlm(NoId); }
        }

        /// <summary>
        ///     是否急停
        /// </summary>
        public override bool IsEmg
        {
            get { return ApsController.IsEmg(NoId); }
        }
        public override void APS_set_command(double pos)
        {
            ApsController.SetCommandPosition(NoId, pos);
            ApsController.SetFeedbackPosition(NoId, pos);
        }
        /// <summary>
        ///     是否到位。
        /// </summary>
        public override bool IsInPosition(double pos)
        {
            //if (NoId == 8 || NoId == 7)
            //{
            //    var tempos = Math.Round(ApsController.GetCurrentCommandPosition(NoId) * Transmission.PulseEquivalent, 3);
            //    return ApsController.IsDown(NoId) & (tempos + 0.20 >= pos & tempos - 0.20 <= pos);
            //}
            //else
            //{
             var tempos = Math.Round(ApsController.GetCurrentFeedbackPosition(NoId) * Transmission.PulseEquivalent, 3);
            //return ApsController.IsDown(NoId) & (tempos + 0.10 >= pos & tempos - 0.10 <= pos);
            if( NoId ==6)//C轴
            return ApsController.IsDown(NoId) & (tempos + 0.20 >= pos & tempos - 0.20 <= pos);
            else return ApsController.IsDown(NoId) & (tempos + 0.01 >= pos & tempos - 0.01 <= pos);
            //}

        }
        public bool IsZYinQuanDown()
        {
            return ApsController.IsDown(NoId);
        }
    }
}