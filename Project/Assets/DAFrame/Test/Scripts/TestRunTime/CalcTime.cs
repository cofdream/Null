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

        public int[] cont;

        delegate void MyAction();
        public void Calc()
        {
            Action temp = Func;
            Start(ref temp);
            return;
            //Action action = null;
            //Delegate @delegate;

            //for (int i = 0; i < cont[2]; i++)
            //{
            //    action += Func;
            //    action += Func2;
            //}

            //@delegate = action;

            //var watch = new Stopwatch();
            //watch.Start();


            //for (int i = 0; i < cont[0]; i++)
            //{
            //    (@delegate as Action)?.Invoke();
            //}

            //watch.Stop();
            //times.Add(watch.ElapsedMilliseconds);
            //Debug.Log("Call @delegate Time:" + watch.ElapsedMilliseconds + " ms.");


            //watch.Reset();
            //Debug.Log("Reset Time:" + watch.ElapsedMilliseconds + " ms.");
            //watch.Start();
            //for (int i = 0; i < cont[1]; i++)
            //{
            //    action?.Invoke();
            //}

            //watch.Stop();
            //times.Add(watch.ElapsedMilliseconds);
            //Debug.Log("Call action Time:" + watch.ElapsedMilliseconds + " ms.");
        }

        public int func;
        public int func2;
        private void Func()
        {
            func++;
        }
        private void Func2()
        {
            func2++;
        }
        public void Call()
        {
            //int a;
        }
        private void Start() { }
        private void Start(ref Action action)
        {
            var list = action.GetInvocationList();

            if (list == null)
            {
                return;
            }
            foreach (var item in list)
            {
                Debug.Log(item);
            }

        }
    }
}