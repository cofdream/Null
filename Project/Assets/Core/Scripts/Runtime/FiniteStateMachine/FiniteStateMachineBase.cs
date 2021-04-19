using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public abstract class FiniteStateMachineBase<T> where T : FiniteStateMachineBase<T>
    {
        public State<T> CurrentState;
    }

}
