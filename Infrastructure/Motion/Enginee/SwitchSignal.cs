using Motion.BoardCard;
using Motion.Interfaces;

namespace Motion.Enginee
{
    /// <summary>
    ///     ��ʾһ����������
    /// </summary>
    public class SwitchSignal : Automatic, ISignal<bool>
    {
        private readonly IoPoint _ioPoint;

        public SwitchSignal(IoPoint ioPoint)
        {
            _ioPoint = ioPoint;
        }

        /// <summary>
        ///     ��ȡ�������ź���ֵ
        /// </summary>
        public bool Value
        {
            get { return _ioPoint.Value; }
            set { _ioPoint.Value = value; }
        }
    }
}