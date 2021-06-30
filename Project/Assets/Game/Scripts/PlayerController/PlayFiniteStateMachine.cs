using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.FSM
{
    [Serializable]
    public class PlayFiniteStateMachine
    {
        public bool State = false;

        [SerializeField] public State[] AllStates;
        [SerializeReference] public State CurrentState;

       
        public void Start(PlayerController PlayerController)
        {
            CurrentState.OnEnter(PlayerController);
            State = true;
        }

        public void Update(PlayerController PlayerController)
        {
            if (!State) return;

            CurrentState.OnUpdate(PlayerController);
        }

        public void FixedUpdate(PlayerController playerController)
        {
            if (!State) return;

            CurrentState.OnFixedUpdate(playerController);
        }
    }
}