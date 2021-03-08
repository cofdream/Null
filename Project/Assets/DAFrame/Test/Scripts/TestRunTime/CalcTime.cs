using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DA.Test
{
    public class CalcTime : MonoBehaviour
    {
        public List<long> times;

        public void Calc()
        {
            int length = 5000000;

            Action a = Call;
            List<Action> actions = new List<Action>(length);
            List<Delegate> delegates = new List<Delegate>(length);


            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < length; i++)
            {
                Delegate @delegate = a;
            }

            watch.Stop();
            times.Add(watch.ElapsedMilliseconds);
            Debug.Log("Call @delegate Time:" + watch.ElapsedMilliseconds + " ms.");


            watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < length; i++)
            {
                actions.Add(a);
            }

            watch.Stop();
            times.Add(watch.ElapsedMilliseconds);
            Debug.Log("Call @delegate Time:" + watch.ElapsedMilliseconds + " ms.");

            watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < length; i++)
            {
                Delegate @delegate = a;
                delegates.Add(@delegate);
            }

            watch.Stop();
            times.Add(watch.ElapsedMilliseconds);
            Debug.Log("Call @delegate Time:" + watch.ElapsedMilliseconds + " ms.");
        }
        public void Call()
        {
            int a;
        }
    }
}