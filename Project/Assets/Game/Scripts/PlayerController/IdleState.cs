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

        public float jumpWaitTime;

        [NonSerialized] private AnimatorData animatorData;


        public override void OnEnter(PlayerController playerController)
        {
            if (animatorData == null) animatorData = new AnimatorData(playerController.Animator);

            SetFootForword(playerController);
        }
        public override void OnUpdate(PlayerController playerController)
        {
            if (playerController.Movement.MoveAmount > 0.001f)
            {
                playerController.TransitionState(LocomotionState);
                return;
            }

            if (playerController.IsJump)
            {
                playerController.IsJump = false;

                playerController.Animator.CrossFade(AnimatorHashes.JumpIdleState, 0.2f);

                playerController.EntWaitState(jumpWaitTime, this);
                return;
            }
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }


        private void SetFootForword(PlayerController playerController)
        {
            playerController.Animator.SetFloat(AnimatorHashes.MoveVerticalParameter, 0);

            Vector3 lf_relative = playerController.ModeTransform.InverseTransformPoint(animatorData.leftFoot.position);
            Vector3 rf_relative = playerController.ModeTransform.InverseTransformPoint(animatorData.rightFoot.position);

            bool leftForward = false;
            if (lf_relative.z > rf_relative.z)
            {
                leftForward = true;
            }
            playerController.Animator.SetBool(AnimatorHashes.LeftFootForwardParameter, !leftForward);
        }
    }
}