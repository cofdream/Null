using UnityEngine;
using System.Collections.Generic;

namespace DevTool
{
    [System.Serializable]
    public class FiniteStateMachinePlayer : FiniteStateMachine<FiniteStateMachinePlayer>
    {
        public float deltaTime;
        public Rigidbody Rigidbody;
        public Transform Transform;
        public Transform CameraTransform;

        public MovementVariable MovementVariable;
        public Vector3 Direction;

        public void Init(State<FiniteStateMachinePlayer> state)
        {
            StateStack = new Stack<State<FiniteStateMachinePlayer>>(8);
            StateStack.Push(state);
            CurrentState = state;
        }
    }
}