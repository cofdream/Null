using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.FSM
{
    [Serializable]
    public class PlayFiniteStateMachine : DAScriptableObject
    {
        public State[] AllStates;
        public State CurrentState;

        public TransformVariable PlayerTransform;
        public AnimatorVariable AnimatorVariable;
        public RigidbodyVariable RigebodyVariable;

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

        public void Initialize(Unit unit)
        {
            PlayerTransform.Value = unit.GameObject.transform;
            AnimatorVariable.Value = unit.Animator;
            RigebodyVariable.Value = unit.Rigidbody;
        }
    }
}