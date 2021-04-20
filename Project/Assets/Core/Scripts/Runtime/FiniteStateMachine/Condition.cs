using UnityEngine;

namespace Core
{
    [System.Serializable]
    public abstract class Condition<T> where T : FiniteStateMachineBase<T>
    {
        public State<T> TargetState;
        public abstract bool Update(T fsm);
    }
}