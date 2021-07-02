using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.FSM
{
    [Serializable]
    public class PlayFiniteStateMachine : ScriptableObject
    {
        [SerializeField] public State[] AllStates;
        [SerializeReference] public State CurrentState;
        [SerializeReference] public WaitState WaitState;

        public void OnStart(PlayerController playerController)
        {
            CurrentState = AllStates[0];
            CurrentState.OnEnter(playerController);
        }

        public void OnUpdate(PlayerController playerController)
        {
            CurrentState.OnUpdate(playerController);
        }
        public void OnFixedUpdate(PlayerController playerController)
        {
            CurrentState.OnFixedUpdate(playerController);
        }

        public void Wait(PlayerController playerController, float waitTime, State endState, bool isUpdate)
        {
            WaitState.WaitTime = waitTime;
            WaitState.TargetState = endState;

            TransitionState(playerController, WaitState, isUpdate);
        }
        public void TransitionState(PlayerController playerController, State targetState, bool isUpdate)
        {
            if (targetState == CurrentState)
            {
                Debug.LogError("无法过渡到自己");
                return;
            }
            CurrentState.OnExit(playerController);
            CurrentState = targetState;
            CurrentState.OnEnter(playerController);
            if (isUpdate)
            {
                CurrentState.OnUpdate(playerController);
            }
        }
    }
}