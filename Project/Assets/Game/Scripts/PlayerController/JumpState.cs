using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class JumpState : State
    {
        public State IdleState;
        public bool isTrainsitionIdleState;

        public float Wait;
        public override void OnEnter(PlayerController playerController)
        {

        }
        public override void OnUpdate(PlayerController playerController)
        {
            Wait += playerController.DeltaTime;
            if (Wait > 3)
            {
                Wait = 0;
                isTrainsitionIdleState = true;
            }
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }

        public override bool CheckTransition(PlayerController playerController, out State targetState)
        {
            if (isTrainsitionIdleState)
            {
                isTrainsitionIdleState = false;
                targetState = IdleState;
                return true;
            }
            targetState = null;
            return false;
        }
    }
}