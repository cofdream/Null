using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.FSM
{
    [Serializable]
    public class PlayFiniteStateMachine : ScriptableObject
    {
        public bool State = false;

        [SerializeField] public State[] AllStates;
        [SerializeReference] public State CurrentState;

        public void OnStart(PlayerController PlayerController)
        {
            CurrentState = AllStates[0];
            CurrentState.OnEnter(PlayerController);
            State = true;
        }

        public void OnUpdate(PlayerController PlayerController)
        {
            if (!State) return;

            CurrentState.OnUpdate(PlayerController);
            if (CurrentState.CheckTransition(PlayerController, out State targetState))
            {
                CurrentState.OnExit(PlayerController);
                CurrentState = targetState;
                CurrentState.OnEnter(PlayerController);
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
            if (!State) return;

            CurrentState.OnFixedUpdate(playerController);
        }
    }
}