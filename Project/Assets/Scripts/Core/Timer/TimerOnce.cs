namespace DA.Timer
{
    /// <summary>
    /// 一次性计时器
    /// </summary>
    public struct TimerOnce : ITimer
    {
        public float TotalTime;
        public float ElapsedTime;
        public TimeCallBack CallBack;

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