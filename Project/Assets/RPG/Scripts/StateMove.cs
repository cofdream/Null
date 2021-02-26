using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class StateMove : FSM.State
    {
        private Transform roleTransform;
        private Transform cameraTransform;

        public MoveController MoveController;

        public bool isRotation;
        private Quaternion _targetRotation;
        public override void OnEnter()
        {
            MoveController.rigidbody.drag = 4;

            roleTransform = MoveController.transform;
            cameraTransform = MoveController.cameraTransform;

            Vector2 move = MoveController.inputActions.Player.Move.ReadValue<Vector2>();

            float horiazontal = move.y;
            float vertical = move.x;

            Vector3 targetDir = cameraTransform.forward * horiazontal + cameraTransform.right * vertical;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = roleTransform.forward;
            }

            _targetRotation = Quaternion.LookRotation(targetDir);

            if (_targetRotation != new Quaternion(0, 0, 0, 0))
            {
                isRotation = true;
                Debug.Log("Play roatation animation");
            }
            else
            {
                isRotation = false;

                MoveController?.animator.SetBool("IsMoving", true);
            }
        }
        public override void OnUpdate()
        {
            if (isRotation)
            {
                Quaternion tr = Quaternion.Lerp(roleTransform.rotation, _targetRotation, MoveController.Delta * MoveController.rotateSpeed);
                roleTransform.rotation = tr;

                float dot = Quaternion.Dot(_targetRotation, tr);
                float offset = 0.999999f;
                if (dot < 0)
                {
                    offset = -0.999999f;
                }
                if (dot > offset)
                {
                    roleTransform.rotation = _targetRotation;

                    isRotation = false;

                    MoveController?.animator.SetBool("IsMoving", true);

                    Debug.LogWarning(Time.time + $"原地旋转End _targetRotation {_targetRotation}   tr {tr} " + (offset < 0 ? "offset < 0" : ""));
                }
                else
                    return;
            }

            Vector2 move = MoveController.inputActions.Player.Move.ReadValue<Vector2>();

            float horiazontal = move.y;
            float vertical = move.x;

            if (horiazontal == 0 && vertical == 0)
            {
                MoveController.rigidbody.drag = 0;

                MoveController.FSM.HandleEvent((int)TranslationIdleType.Move_To_Idle);
                return;
            }
            else
            {
                MoveController.rigidbody.drag = 4;
            }

            float moveAmount = Mathf.Clamp01(Mathf.Abs(horiazontal) + Mathf.Abs(vertical));


            // MovementForward
            Vector3 targetVelocity = roleTransform.forward * moveAmount * MoveController.walkSpeed;
            targetVelocity.y = MoveController.rigidbody.velocity.y;

            MoveController.rigidbody.velocity = targetVelocity;


            // RotationBasedOnCameraOrientation
            Vector3 targetDir = cameraTransform.forward * horiazontal + cameraTransform.right * vertical;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = roleTransform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            targetRotation = Quaternion.Slerp(roleTransform.rotation, targetRotation, MoveController.Delta * MoveController.walkRotationSpeed * moveAmount);
            roleTransform.rotation = targetRotation;
        }

        public override void OnExit()
        {

            MoveController?.animator.SetBool("IsMoving", false);
        }
    }
}