using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class IdleState : State
    {
        public float JumpSpeed;

        public State LocomotionState;
        [SerializeReference] public bool isTransitionLocomotionState;

        public State JumpState;
        [SerializeReference] public bool isTransitionJumpState;

        public WaitState waitState;
        [SerializeReference] public bool isTransitionWaitState;
        public float WaitTime;
        [SerializeReference] public bool startJump;


        public override void OnEnter(PlayerController playerController)
        {
            Debug.Log("Enter Idle");

            playerController.Animator.SetBool(AnimatorHashes.stop, true);
        }
        public override void OnUpdate(PlayerController playerController)
        {
            if (startJump)
            {
                startJump = false;
                playerController.Rigidbody.velocity += JumpSpeed * Vector3.up;

                isTransitionJumpState = true;
                Debug.Log("Jump up");
                return;
            }

            isTransitionLocomotionState = playerController.Movement.MoveAmount > 0.01f;

            if (playerController.IsJump)
            {
                playerController.IsJump = false;

                playerController.Animator.CrossFade(AnimatorHashes.JumpIdleState, 0.2f);

                startJump = true;

                isTransitionWaitState = true;
                waitState.WaitTime = WaitTime;
                waitState.TargetState = this;
                Debug.Log("Jump Ani-");
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
            targetState = null;

            if (isTransitionJumpState)
            {
                isTransitionJumpState = false;
                targetState = JumpState;
                return true;
            }

            if (isTransitionWaitState)
            {
                isTransitionWaitState = false;
                targetState = waitState;
                return true;
            }

            if (isTransitionLocomotionState)
            {
                isTransitionLocomotionState = false;
                targetState = LocomotionState;
                return true;
            }

            return false;
        }
    }
}