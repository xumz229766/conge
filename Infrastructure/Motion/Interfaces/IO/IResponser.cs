using System.Toolkit.Interfaces;
namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     表示一个响应器。
    /// </summary>
    public interface IResponser<T> : IAutomatic where T : struct
    {
        bool Value { set; }
    }
}