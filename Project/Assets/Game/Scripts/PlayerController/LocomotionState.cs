using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LocomotionState : State
    {
        //public float WalkSpeed;
        public float RunSpeed;
        public float JumpSpeed;
        public float RotateSpeed;

        public State IdleState;


        private Transform cameraTransform;

        public State RotateBack;

        public override void OnEnter(PlayerController playerController)
        {
            cameraTransform = playerController.CameraHangPoint.Camera.transform;

            //原地跳以后 发现跳转移动会顿一下， 猜测是过渡到locomation异常，进入以后设置一下这个状态值
            playerController.Animator.CrossFade(AnimatorHashes.LocomotionState, 0);
        }
        public override void OnUpdate(PlayerController playerController)
        {
            if (playerController.Movement.MoveAmount < 0.01f)
            {
                playerController.TransitionState(IdleState, false);
                return;
            }

            RotationBaseOnCameraOrientaion(playerController);

            Movement(playerController);
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {
            playerController.Rigidbody.drag = 0;
        }

        private void Movement(PlayerController playerController)
        {

            if (playerController.IsRun)
            {
                playerController.Movement.MoveSpeed = RunSpeed;
            }
            else
            {
                playerController.Movement.MoveSpeed = RunSpeed * 2;
            }

            playerController.Rigidbody.drag = 4;

            Vector3 targetVelocity = playerController.Movement.MoveAmount * playerController.Movement.MoveSpeed * playerController.transform.forward;

            #region  jump

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            #endregion

            playerController.Rigidbody.velocity = targetVelocity;

            //Ani
            playerController.Animator.SetFloat(AnimatorHashes.MoveVerticalParameter, playerController.Movement.MoveAmount, 0.2f, playerController.DeltaTime);
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

            //QuickTurnLeft
            if (Vector3.Angle(playerController.TargetDirection, targetDirection) > 179f)
            {
                playerController.TargetDirection = targetDirection;
                playerController.Animator.CrossFade(AnimatorHashes.QuickTurnLeftState, 0.2f);

                playerController.TransitionState(RotateBack);
                return;
            }

            playerController.TargetDirection = targetDirection;

            Quaternion tragetRotation = Quaternion.LookRotation(targetDirection);
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, tragetRotation, playerController.DeltaTime * playerController.Movement.MoveAmount * RotateSpeed);
        }

    }
}