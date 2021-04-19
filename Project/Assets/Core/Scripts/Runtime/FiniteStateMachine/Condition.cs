using UnityEngine;

namespace Core
{
    public abstract class Condition<T> where T : FiniteStateMachineBase<T>
    {
        public State<T,StateAction<T>> TargetState;
        public abstract bool Update(T fsm);
    }
}