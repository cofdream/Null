using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LocomotionState : State
    {
        public float JumpSpeed;
        public float WalkSpeed;
        public float RunSpeed;

        public float RotateSpeed;

        public bool isTransitionIdleState;
        public State IdleState;

        [NonSerialized] private AnimatorData animatorData;

        private Transform cameraTransform;

        private Vector3 lastTargetDirection;
        public bool isTransfitionQuickTurnRotateBack;
        public State RotateBack;

        public override void OnEnter(PlayerController playerController)
        {

            Debug.Log("Enter Locomotion");
            cameraTransform = playerController.CameraHangPoint.Camera.transform;

            if (animatorData == null)
            {
                animatorData = new AnimatorData(playerController.Animator);
            }


            playerController.Animator.SetBool(AnimatorHashes.stop, false);
        }
        public override void OnUpdate(PlayerController playerController)
        {

            Movement(playerController);
            RotationBaseOnCameraOrientaion(playerController);

            if (playerController.IsRun)
                playerController.Movement.MoveSpeed = RunSpeed;
            else
                playerController.Movement.MoveSpeed = WalkSpeed;


            playerController.Animator.SetFloat(AnimatorHashes.MoveVerticalParameter, playerController.Movement.MoveAmount, 0.2f, playerController.DeltaTime);

            isTransitionIdleState = playerController.Movement.MoveAmount < 0.01f;

            if (isTransitionIdleState)
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
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }
        public override bool CheckTransition(PlayerController playerController, out State targetState)
        {
            if (isTransfitionQuickTurnRotateBack)
            {
                isTransfitionQuickTurnRotateBack = false;
                targetState = RotateBack;
                return true;
            }

            if (isTransitionIdleState)
            {
                isTransitionIdleState = false;
                targetState = IdleState;
                return true;
            }

            targetState = null;
            return false;
        }

        private void Movement(PlayerController playerController)
        {
            if (playerController.Movement.MoveAmount > 0.1f)
            {
                playerController.Rigidbody.drag = 0;
            }
            else
            {
                playerController.Rigidbody.drag = 4;
            }

            Vector3 targetVelocity = playerController.Movement.MoveAmount * playerController.Movement.MoveSpeed * playerController.transform.forward;

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            playerController.Rigidbody.velocity = targetVelocity;
        }

        private void RotationBaseOnCameraOrientaion(PlayerController playerController)
        {
            float h = playerController.Movement.Horizontal;
            float v = playerController.Movement.Vertical;

            Vector3 targetDirection = cameraTransform.forward * v + cameraTransform.right * h;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = playerController.transform.forward;
            }

            playerController.TargetDirection = targetDirection;

            //QuickTurnLeft
            if (Math.Abs(Vector3.Angle(lastTargetDirection, targetDirection)) > 179f)
            {
                isTransfitionQuickTurnRotateBack = true;
                lastTargetDirection = targetDirection;

                //playerController.Animator.applyRootMotion = true;
                playerController.Animator.CrossFade(AnimatorHashes.QuickTurnLeftState, 0.2f);
                Debug.Log("TurnLeft");
                return;
            }

            lastTargetDirection = targetDirection;

            Quaternion tragetRotation = Quaternion.LookRotation(targetDirection);
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, tragetRotation, playerController.DeltaTime * playerController.Movement.MoveAmount * RotateSpeed);
        }

    }
}