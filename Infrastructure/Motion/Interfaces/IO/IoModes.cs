using System;

namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     IO 端口类型
    /// </summary>
    [Flags]
    public enum IoModes
    {
        /// <summary>
        ///     本地只读端口
        /// </summary>
        Senser = 0x1,

        /// <summary>
        ///     本地只写端口
        /// </summary>
        Responser = 0x2,

        /// <summary>
        ///     本地读写端口
        /// </summary>
        Signal = 0x3,
    }
}