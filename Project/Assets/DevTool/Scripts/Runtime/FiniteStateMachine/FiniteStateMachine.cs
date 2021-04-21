using System.Collections.Generic;
using UnityEngine;

namespace DevTool
{
    [System.Serializable]
    public class FiniteStateMachine<T> where T : FiniteStateMachine<T>
    {
        public State<T> CurrentState;

        [SerializeField] protected Stack<State<T>> StateStack;

        public static void EnterNextState(State<T> state, T fsm) 
        {
            var endState = fsm.StateStack.Peek();
            endState.Exit(fsm);

            fsm.StateStack.Push(state);
            state.Enter(fsm);

            fsm.CurrentState = state;
        }

        public static void EnterLastState(T fsm)
        {
            var endState = fsm.StateStack.Pop();
            endState.Exit(fsm);

            var lastState = fsm.StateStack.Peek();
            lastState.Enter(fsm);

            fsm.CurrentState = lastState;
        }
    }
}
