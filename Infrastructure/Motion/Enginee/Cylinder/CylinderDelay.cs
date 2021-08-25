using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Applications
{
    public class CylinderDelay
    {
        /// <summary>
        /// 原点到位时间
        /// </summary>
        public int OriginTime { get; set; }
        /// <summary>
        /// 动点到时时间
        /// </summary>
        public int MoveTime { get; set; }
        /// <summary>
        /// 报警延时
        /// </summary>
        public int AlarmTime { get; set; }
    }
}
