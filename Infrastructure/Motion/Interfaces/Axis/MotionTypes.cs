using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Axis
{
    /// <summary>
    /// 轴运动类型
    /// </summary>
    public enum  MotionTypes :byte
    {
        直线插补,
        圆弧插补
    }
    public struct CardType
    {
        public ushort CardID;
        public ushort AxisID;
    }
}
