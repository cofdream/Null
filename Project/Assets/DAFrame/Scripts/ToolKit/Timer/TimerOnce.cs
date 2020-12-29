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
        public event Action Callback;

        public TimerOnce(Action callback, float waitTime, float useTime = 0f)
        {
            TotalTime = waitTime;
            ElapsedTime = useTime;
            Callback = callback;
        }

        public void Run()
        {
            Timer.AddTimer(this);
        }

        public bool Update(float time)
        {
            ElapsedTime += time;
            if (TotalTime <= ElapsedTime)
            {
                Callback?.Invoke();
                return true;
            }
            return false;
        }
        public void Dispose()
        {

        }
    }

}