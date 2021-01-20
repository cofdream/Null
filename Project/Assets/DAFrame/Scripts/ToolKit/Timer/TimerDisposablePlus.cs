using System;

namespace DA.Timer
{
    /// <summary>
    /// 一次性计时器Plus
    /// </summary>
    public struct TimerDisposablePlus : ITimer
    {
        public float WaitingTime;
        public float ElapsedTime;
        public Action CallBack;
        public bool Pause;

        public bool Update(float time)
        {
            if (Pause) return false;

            ElapsedTime += time;
            if (WaitingTime <= ElapsedTime)
            {
                CallBack?.Invoke();
                return true;
            }
            return false;
        }
        public void Start()
        {
            Timer.StartTimer(this);
        }
        public void Reset()
        {

        }
        public void Dispose()
        {

        }
    }
}