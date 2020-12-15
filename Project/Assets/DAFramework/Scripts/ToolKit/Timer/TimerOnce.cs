using System;

namespace DA.Timer
{
    /// <summary>
    /// 一次性计时器
    /// </summary>
    public struct TimerOnce : ITimer
    {
        /// <summary>
        /// 等待时间
        /// </summary>
        public float TotalTime;
        /// <summary>
        /// 流逝时间
        /// </summary>
        public float ElapsedTime;
        public Action CallBack;

        public bool Update(float time)
        {
            ElapsedTime += time;
            if (TotalTime <= ElapsedTime)
            {
                CallBack?.Invoke();
                return true;
            }
            return false;
        }
        public void Dispose()
        {

        }
    }

}