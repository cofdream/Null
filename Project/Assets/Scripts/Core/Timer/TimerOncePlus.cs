namespace DA.Timer
{
    /// <summary>
    /// 一次性计时器Plus
    /// </summary>
    public struct TimerOncePlus : ITimer
    {
        public float TotalTime;
        public float ElapsedTime;
        public TimeCallBack CallBack;
        public bool Pause;

        public bool Update(float time)
        {
            if (Pause) return false;

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