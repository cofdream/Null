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
        public override bool CheckTransition(PlayerController playerController, out State targetState)
        {
            curWaitTime += playerController.DeltaTime;
            if (curWaitTime >= WaitTime)
            {
                targetState = TargetState;
                WaitTime = 0;
                curWaitTime = 0;
                TargetState = null;
                return true;
            }
            targetState = null;
            return false;
        }
    }
}