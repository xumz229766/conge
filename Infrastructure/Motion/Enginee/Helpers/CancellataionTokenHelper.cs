using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using System;
namespace CMotion.Applications
{
    public static class CancellataionTokenHelper
    {
        private static readonly object SyncRoot = new object();

        /// <summary>
        ///     超时判读集合。
        /// </summary>
        public static ObservableCollection<Judger> TimeoutJudgers = new ObservableCollection<Judger>();

        /// <summary>
        ///     等待条件判读成立。
        /// </summary>
        /// <param name="cancelToken">取消判读操作关联的令牌。</param>
        /// <param name="timeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
        /// <param name="waitHandles">要继续的 <see cref="T:System.Threading.WaitHandle" />。</param>
        /// <returns></returns>
        public static bool WaitAll(this CancellationToken cancelToken, int timeout = -1, params WaitHandle[] waitHandles)
        {            
            return WaitHandle.WaitAll(waitHandles, timeout);
        }
        /// <summary>
        ///     等待条件判读成立。
        /// </summary>
        /// <param name="cancelToken">取消判读操作关联的令牌。</param>
        /// <param name="timeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
        /// <param name="waitHandles">要继续的 <see cref="T:System.Threading.WaitHandle" />。</param>
        /// <returns></returns>
        public static bool WaitAll(this CancellationToken cancelToken, int timeout = -1, Func<bool> exitCondition = null, params WaitHandle[] waitHandles)
        {
            Judger judger = new Judger(exitCondition);
            while (true)
            {
                var result = judger.Sure(cancelToken, timeout);
                if (result.IsCompleted)
                {
                    lock (SyncRoot)
                    {
                        if (TimeoutJudgers.Contains(judger))
                        {
                            TimeoutJudgers.Remove(judger);
                        }
                    }
                    break;
                }
                lock (SyncRoot)
                {
                    if (!TimeoutJudgers.Contains(judger))
                    {
                        TimeoutJudgers.Add(judger);
                    }
                }
            }
            return WaitHandle.WaitAll(waitHandles, timeout);
        }
        /// <summary>
        ///     等待条件判读成立。
        /// </summary>
        /// <param name="cancelToken">取消判读操作关联的令牌。</param>
        /// <param name="timeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
        /// <param name="waitHandles">要继续的 <see cref="T:System.Threading.WaitHandle" />。</param>
        /// <returns></returns>
        public static int WaitAny(this CancellationToken cancelToken, int timeout = -1, params WaitHandle[] waitHandles)
        {
            return WaitHandle.WaitAny(waitHandles, timeout);
        }
        /// <summary>
        ///     等待条件判读成立。
        /// </summary>
        /// <param name="cancelToken">取消判读操作关联的令牌。</param>
        /// <param name="timeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
        /// <param name="waitHandles">要继续的 <see cref="T:System.Threading.WaitHandle" />。</param>
        /// <returns></returns>
        public static int WaitAny(this CancellationToken cancelToken, int timeout = -1, Func<bool> exitCondition = null, params WaitHandle[] waitHandles)
        {
            Judger judger = new Judger(exitCondition);
            while (true)
            {
                var result = judger.Sure(cancelToken, timeout);
                if (result.IsCompleted)
                {
                    lock (SyncRoot)
                    {
                        if (TimeoutJudgers.Contains(judger))
                        {
                            TimeoutJudgers.Remove(judger);
                        }
                    }
                    break;
                }

                lock (SyncRoot)
                {
                    if (!TimeoutJudgers.Contains(judger))
                    {
                        TimeoutJudgers.Add(judger);
                    }
                }
            }
            return WaitHandle.WaitAny(waitHandles, timeout);
        }
    }
}

