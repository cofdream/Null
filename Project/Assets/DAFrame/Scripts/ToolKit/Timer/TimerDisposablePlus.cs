using System;

namespace DA.Timer
{
    /// <summary>
    /// 一次性计时器Plus
    /// </summary>
    public struct TimerDisposablePlus : ITimer
    {
        /// <summary>
        /// 每次更新等待的时间
        /// </summary>
        public float WaitingTime;
        /// <summary>
        /// 已经过的时间
        /// </summary>
        public float ElapsedTime;
        /// <summary>
        /// 计时回调
        /// </summary>
        public Action Callback;
        /// <summary>
        /// 暂停
        /// </summary>
        public bool IsPause;

        public TimerDisposablePlus(float waitingTime, Action callback, float useTime = 0f)
        {
            WaitingTime = waitingTime;
            ElapsedTime = useTime;
            Callback = callback;
            IsPause = false;
        }

        public bool Update(float time)
        {
            if (IsPause) return false;

            ElapsedTime += time;
            if (WaitingTime <= ElapsedTime)
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