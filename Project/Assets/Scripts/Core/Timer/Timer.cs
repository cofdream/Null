using System;
using System.Collections.Generic;

namespace DA.Timer
{
    public sealed class Timer
    {
        public static List<ITimer> timers;

        static Timer()
        {
            timers = new List<ITimer>(10);
            MonoController.UpdataAction += UpdateTime;
        }

        public static void UpdateTime()
        {
            var elapsedTime = MonoController.DelateTime;

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
        }
    }
}