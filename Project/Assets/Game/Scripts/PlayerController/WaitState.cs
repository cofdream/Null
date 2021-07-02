using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class WaitState : State
    {
        public float WaitTime;
        public State TargetState;

        [SerializeField] private float curWaitTime;
        public override void OnUpdate(PlayerController playerController)
        {
            curWaitTime += playerController.DeltaTime;
            if (curWaitTime >= WaitTime)
            {
                WaitTime = 0;
                curWaitTime = 0;
                playerController.TransitionState(TargetState);
            }
        }
    }
}