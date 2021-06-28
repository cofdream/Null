using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.FSM
{
    [Serializable]
    public class FiniteStateMachine : DAScriptableObject
    {
        public State[] AllStates;
        public State CurrentState;

        public void Update()
        {
            if (CurrentState == null)
            {
                return;
            }
            CurrentState.OnUpdate();
            var targetState = CurrentState.CheckTransitions();
            if (targetState != null)
            {
                ChangeState(targetState);
            }
        }

        public void FixedUpdate()
        {
            if (CurrentState == null)
            {
                return;
            }
            CurrentState.OnFixedUpdate();
        }

        public void ChangeState(State targetState)
        {
            CurrentState.OnExit();
            targetState.OnEnter();
            CurrentState = targetState;
        }
    }
}