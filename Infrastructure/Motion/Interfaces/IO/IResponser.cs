using System.Toolkit.Interfaces;
namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     ��ʾһ����Ӧ����
    /// </summary>
    public interface IResponser<T> : IAutomatic where T : struct
    {
        bool Value { set; }
    }
}