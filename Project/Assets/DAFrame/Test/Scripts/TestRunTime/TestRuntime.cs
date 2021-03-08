using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DA.Test
{
    internal class TestRuntime : MonoBehaviour
    {
        public bool Start = false;
        public UnityEvent CalcRuntimeEvent;
        public void Update()
        {
            if (Start)
            {
                Start = false;
                CalcRuntimeEvent?.Invoke();
            }
        }
    }
}