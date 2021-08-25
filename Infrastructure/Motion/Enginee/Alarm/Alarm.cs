using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Applications
{
    /// <summary>
    /// 报警信息判断
    /// </summary>
    public class Alarm
    {
        private readonly Func<bool> _condition;
        public Alarm(Func<bool> condition)
        {
            _condition = condition;
        }
        public External External { get; set; }
        /// <summary>
        ///     报警级别
        /// </summary>
        public AlarmLevels AlarmLevel { get; set; }
        /// <summary>
        /// 是否报警
        /// </summary>
        public bool AlarmJugger()
        {
            try
            {
                if (External.AlarmReset) IsFired = false;
                if (!IsFired) IsFired = _condition();
            }
            catch (Exception)
            {
                IsFired = true;
            }
            return IsFired;
        }
        /// <summary>
        /// 报警是否激活
        /// </summary>
        public bool IsFired { get; set; }
        /// <summary>
        /// 报警名称
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 报警类型
    /// </summary>
    public struct AlarmType
    {
        public bool IsAlarm;
        public bool IsPrompt;
        public bool IsWarning;
    }
}
