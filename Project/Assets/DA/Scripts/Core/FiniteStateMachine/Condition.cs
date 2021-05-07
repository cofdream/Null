using UnityEngine;

namespace DevTool
{
    [System.Serializable]
    public abstract class Condition<T> where T : FiniteStateMachine<T>
    {
        public State<T> TargetState;
        public abstract bool Update(T fsm);
    }
}