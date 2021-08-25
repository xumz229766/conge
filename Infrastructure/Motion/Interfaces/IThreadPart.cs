namespace CMotion.Interfaces
{
    /// <summary>
    ///     线程部件
    /// </summary>
    public interface IThreadPart
    {
        /// <summary>
        ///     运行任务线程
        /// </summary>
        /// <param name="runningMode">运行模式</param>
        void Run();
    }
}