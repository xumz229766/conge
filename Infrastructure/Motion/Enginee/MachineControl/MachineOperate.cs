using System;
using CMotion.Interfaces.IO;
using System.Diagnostics;
namespace CMotion.Applications
{
    public class MachineOperate
    {
        private readonly Func<bool> m_condition;
        private readonly Func<bool> m_isAlarm;
        private bool m_NotReady, m_Ready, m_sign;
        public MachineOperate(Func<bool> Condition, Func<bool> IsAlarm)
        {
            m_condition = Condition;
            m_isAlarm = IsAlarm;
        }
        public EventButton StartButton { private get; set; }
        public EventButton StopButton { private get; set; }
        public EventButton PauseButton { private get; set; }
        public EventButton ResetButton { private get; set; }
        public EventButton EstopButton { private get; set; }
        public bool Start { private get; set; }
        public bool Stop { private get; set; }
        public bool Pause { private get; set; }
        public bool Reset { private get; set; }
        public bool CleanProductDone { get; set; }
        public bool ManualAutoModel { private get; set; }
        public bool IniliazieDone { get; set; }
        public bool RunningSign { get; set; }
        public bool Running { get; private set; }
        public bool Pausing { get; private set; }
        public bool Stopping { get; set; }
        public bool Resetting { get; private set; }
        public bool Alarming { get; private set; }
        public int Flow { get; set; }
        public MachineStatus Status { get; private set; }
        public void Run()
        {
            //获取执行条件
            var _condition = m_condition();
            //获取故障状态
            var _isAlarm = m_isAlarm() || Flow == -1;
            StartButton.IsPressed = StartButton.PressedIO == null ? false : StartButton.PressedIO.Value;
            StopButton.IsPressed = StopButton.PressedIO == null ? false : StopButton.PressedIO.Value;
            PauseButton.IsPressed = PauseButton.PressedIO == null ? false : PauseButton.PressedIO.Value;
            ResetButton.IsPressed = ResetButton.PressedIO == null ? false : ResetButton.PressedIO.Value;
            //EstopButton.IsPressed = EstopButton.PressedIO.Value;
            //启动标记
            if (Start && ManualAutoModel && !Pause && EstopButton.PressedIO.Value && _condition && !_isAlarm && IniliazieDone)
                RunningSign = true;
            //启动中
            if ((Start || Running) && ManualAutoModel && !Pause &&
                EstopButton.PressedIO.Value && _condition && !_isAlarm && IniliazieDone && RunningSign)
                Running = true;
            else
                Running = false;

            if (!EstopButton.PressedIO.Value || CleanProductDone) RunningSign = false;
            //停止中
            if ((Stop || Stopping) && !ManualAutoModel && !Reset && !Start && _condition && !_isAlarm && EstopButton.PressedIO.Value)
                Stopping = true;
            else
                Stopping = false;
            //暂停中
            if ((Pause || _isAlarm || Pausing) && ManualAutoModel && !Start && _condition && EstopButton.PressedIO.Value)
                Pausing = true;
            else
                Pausing = false;

            if (!m_sign && Reset)
            {
                Flow = 0;
                m_sign = true;
            }
            if (!Reset) m_sign = false;
            if ((Reset || Resetting) && !Stop && !IniliazieDone && !_isAlarm && EstopButton.PressedIO.Value)
                Resetting = true;
            else
                Resetting = false;

            if (_isAlarm) Alarming = true;
            else Alarming = false;

            if (!IniliazieDone && !_isAlarm && EstopButton.PressedIO.Value) m_NotReady = true;
            else m_NotReady = false;

            if (IniliazieDone && !_isAlarm && !Running && !Stopping && !Pausing && !Resetting)
                m_Ready = true;
            else
                m_Ready = false;

            if (m_NotReady) Status = MachineStatus.设备未准备好;
            if (m_Ready) Status = MachineStatus.设备准备好;
            if (Running) Status = MachineStatus.设备运行中;
            if (Stopping) Status = MachineStatus.设备停止中;
            if (Pausing && !_isAlarm) Status = MachineStatus.设备暂停中;
            if (Resetting) Status = MachineStatus.设备复位中;
            if (Alarming) Status = MachineStatus.设备报警中;
            if (!EstopButton.PressedIO.Value) Status = MachineStatus.设备急停已按下;
        }
    }
}
