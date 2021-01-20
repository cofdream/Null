using System;

namespace DA.Timer
{
    public struct TimerDisposable : ITimer
    {
        public float WaitingTime;
        public float ElapsedTime;
        public event Action Callback;

        public TimerDisposable(float waitingTime, Action callback, float useTime = 0f)
        {
            WaitingTime = waitingTime;
            ElapsedTime = useTime;
            Callback = callback;
        }

        public void Start()
        {
            Timer.StartTimer(this);
        }

        public bool Update(float time)
        {
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