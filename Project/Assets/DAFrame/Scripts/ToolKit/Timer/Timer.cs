using System;
using System.Collections.Generic;

namespace DA.Timer
{
    public static class Timer
    {
        public static List<ITimer> timers;

        private static int minCapacity = 20;
        
        private static float disposeCurrentTime;// 释放时间计时 单位：s
        private static float disposeTotalTime = 3f;//60  // 释放时间 单位：s

        static Timer()
        {
            timers = new List<ITimer>(minCapacity);

            FrameUpdater.UpdataAction += UpdateTime;
        }

        public static void UpdateTime()
        {
            var elapsedTime = FrameUpdater.DelateTime;

            int length = timers.Count;
            for (int i = 0; i < length; i++)
            {
                var timer = timers[i];
                bool isRemove = timer.Update(elapsedTime);
                if (isRemove)
                {
                    timer.Dispose();
                    length--;
                    timers[i] = timers[length];
                    timers.RemoveAt(length);
                }
                else
                {
                    timers[i] = timer;
                }
            }

            // 在指定释放时间内，检测池大小是否过大。释放一半数组
            int capacity = timers.Capacity;
            if (length < capacity * 0.25f)
            {
                disposeCurrentTime += elapsedTime;
                if (disposeCurrentTime > disposeTotalTime)
                {
                    disposeCurrentTime = 0;
                    int curCapacity = (int)(length * 0.5f);
                    timers.Capacity = curCapacity > minCapacity ? curCapacity : minCapacity;
                }
            }
            else
            {
                disposeCurrentTime = 0f;
            }
        }

        public static void AddTimer(ITimer timer)
        {
            timers.Add(timer);
        }
        public static void AddTimers(IEnumerable<ITimer> timer)
        {
            timers.AddRange(timer);
        }
    }
}