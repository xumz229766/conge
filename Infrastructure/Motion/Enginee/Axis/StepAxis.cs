using System;
using CMotion.Interfaces;
namespace CMotion.Applications
{
    /// <summary>
    ///  步进电机轴
    /// </summary>
    public class StepAxis : ApsAxis
    {
        public StepAxis(IApsController apsController) : base(apsController)
        {
        }
        public override double CurrentPos
        {
            get
            {
                var Data = ApsController.GetCurrentCommandPosition(NoId) * Transmission.PulseEquivalent;
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

        public override void APS_set_command(double pos)
        {
            //base.APS_set_command(pos);
            ApsController.SetCommandPosition(NoId, pos);
        }
        /// <summary>
        ///     是否到位。
        /// </summary>
        public override bool IsInPosition(double pos)
        {
            //var tempos = Math.Round(ApsController.GetCurrentCommandPosition(NoId) * Transmission.PulseEquivalent, 3);
            return ApsController.IsDown(NoId) & (CurrentPos - 0.020 < pos && CurrentPos + 0.02 > pos);
        }
    }
}