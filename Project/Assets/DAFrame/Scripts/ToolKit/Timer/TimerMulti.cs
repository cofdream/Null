using System;

namespace DA.Timer
{
    /// <summary>
    /// 可多次计时器
    /// </summary>
    public struct TimerMulti : ITimer
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
        /// 剩余需要计时间的次数
        /// </summary>
        public ushort Number;
        /// <summary>
        /// 计时回调
        /// </summary>
        public Action Callback;

        public TimerMulti(float waitingTime, Action callback, ushort number = 1, float useTime = 0f)
        {
            WaitingTime = waitingTime;
            ElapsedTime = useTime;
            Number = number;
            Callback = callback;
        }
        public bool Update(float time)
        {
            ElapsedTime += time;
            if (WaitingTime <= ElapsedTime)
            {
                Callback?.Invoke();
                Number--;
                if (Number == 0)
                {
                    // 暂时保留插值
                    ElapsedTime = WaitingTime - ElapsedTime;
                    return true;
                }
                return false;
            }
            return false;
        }
        public void Dispose()
        {

        }
    }
}