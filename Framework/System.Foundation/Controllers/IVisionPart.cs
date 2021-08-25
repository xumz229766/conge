using System;
using System.Foundation.Collections;
using System.Threading.Tasks;

namespace System.Foundation.Controllers
{
    /// <summary>
    ///     运动部件。
    /// </summary>
    public interface IVisionPart : IPart
    {
        /// <summary>
        ///     获取一个值，表示部件是否在运行中。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        ///     获取一个值，表示部件是否在持续。
        /// </summary>
        bool IsContinue { get; }

        /// <summary>
        ///     运行过程。
        /// </summary>
        Task RunningAsync();
    }
}
