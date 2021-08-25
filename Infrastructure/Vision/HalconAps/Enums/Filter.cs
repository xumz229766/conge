using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.HalconAps.Enums
{
    /// <summary>
	/// 滤波方法
	/// </summary>
	public enum Filter
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 中值
        /// </summary>
        Median,
        /// <summary>
        /// 均值
        /// </summary>
        Mean,
        /// <summary>
        /// 高斯
        /// </summary>
        Gauss,
        /// <summary>
        /// 平滑
        /// </summary>
        Smoothing
    }
}
