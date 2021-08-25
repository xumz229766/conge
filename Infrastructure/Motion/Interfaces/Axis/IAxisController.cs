using System.Collections.Generic;
using CMotion.Interfaces.Configuration;
namespace CMotion.Interfaces.Axis
{
    /// <summary>
    ///     运动轴插补控制器。
    /// </summary>
    public interface IAxisController : IAxisController<VelocityCurve, MoveDirection>
    {
    }

    /// <summary>
    ///     运动轴插补控制器。
    /// </summary>
    /// <typeparam name="T">速度曲线参数</typeparam>
    /// <typeparam name="V">运动方向参数</typeparam>
    public interface IAxisController<in T, in V>
    {
        /// <summary>
        ///     是否报警
        /// </summary>
        /// <returns></returns>
        bool IsAlm(int axisNo);
        /// <summary>
        ///     是否到达正限位
        /// </summary>
        /// <returns></returns>
        bool IsPel(int axisNo);
        /// <summary>
        ///     是否到达正负位
        /// </summary>
        /// <returns></returns>
        bool IsMel(int axisNo);
        /// <summary>
        ///     是否在轴原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        bool IsOrg(int axisNo);
        /// <summary>
        ///     是否急停
        /// </summary>
        /// <returns></returns>
        bool IsEmg(int axisNo);
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        bool IsSZ(int axisNo);
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        bool IsINP(int axisNo);
        /// <summary>
        ///     获取电机励磁状态。
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        bool GetServo(int axisNo);
        /// <summary>
        ///     获取轴当前位置
        /// </summary>
        /// <param name="axisNo">轴标识</param>
        /// <returns>当前位置</returns>
        int GetCurrentCommandPosition(int axisNo);
        int GetCurrentFeedbackPosition(int axisNo);
        int GetCurrentCommandSpeed(int axisNo);
        int GetCurrentFeedbackSpeed(int axisNo);
        /// <summary>
        ///     设置指令位置计数器计数值
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        void SetCommandPosition(int axisNo, double position);
        /// <summary>
        ///     设置指令位置计数器计数值
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        void SetFeedbackPosition(int axisNo, double position);
        void CleanError(int axisNo);
        int GetState(int axisNo);
        /// <summary>
        ///     轴上电
        /// </summary>
        /// <param name="noId"></param>
        void ServoOn(int axisNo);
        /// <summary>
        ///     轴掉电
        /// </summary>
        /// <param name="noId"></param>
        void ServoOff(int axisNo);
        /// <summary>
        ///  设置轴速度
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        void SetAxisVelocity(int axisNo, T velocityCurveParams);
        /// <summary>
        ///  设置轴速度
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        void SetAxisHomeVelocity(int axisNo, HomeParams homeParams);
        /// <summary>
        ///     单轴相对运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        /// <returns></returns>
        void MoveRelPulse(int axisNo, double position, T velocityCurveParams);

        /// <summary>
        ///     单轴绝对运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        /// <param name="velocityCurveParams"></param>
        void MoveAbsPulse(int axisNo, double position, T velocityCurveParams);
        /// <summary>
        ///     连续运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="moveDirection"></param>
        /// <param name="velocityCurveParams"></param>
        /// <returns></returns>
        void ContinuousMove(int axisNo, V moveDirection, T velocityCurveParams);
        /// <summary>
        ///     立即停止
        /// </summary>
        /// <param name="axisNo"></param>
        void ImmediateStop(int axisNo);
        /// <summary>
        ///     减速停止指定机构轴脉冲输出
        /// </summary>
        /// <param name="axisNo"></param>
        void DecelStop(int axisNo);
        /// <summary>
        ///     是否停止移动
        /// </summary>
        /// <returns></returns>
        bool IsDown(int axisNo, bool hasExtEncode = false);
        /// <summary>
        ///     检测指定轴的运动状态
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="hasExtEncode">是否有编码器接入(步进电机无外部编码器)</param>
        /// <remarks>判断INP鑫海</remarks>
        int CheckDone(int axisNo, double timeoutLimit, bool hasExtEncode = false);
        /// <summary>
        ///     回零
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="homeParams">回原点方式</param>
        /// <param name="DirMode">0:DIR_CW,1:DIR_CCW</param>
        void BackHome(int axisNo, HomeParams homeParams);
        bool IsHoming(int axisNo);
        /// <summary>
        ///     检查回零是否完成
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        int CheckHomeDone(int axisNo, double timeoutLimit);
    }
}