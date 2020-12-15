using System;

namespace DA.Timer
{
    /// <summary>
    /// 多次计时器Plus
    /// </summary>
    public struct TimerMultiPlus : ITimer
    {
        public float TotalTime;
        public float ElapsedTime;
        public ushort Count;
        public Action CallBack;
        public bool Pause;

        public bool Update(float time)
        {
            if (Pause) return false;

            ElapsedTime += time;
            if (TotalTime <= ElapsedTime)
            {
                CallBack?.Invoke();
                Count--;
                if (Count == 0)
                {
                    // 暂时保留插值
                    ElapsedTime = TotalTime - ElapsedTime;
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