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
            }
            else
            {
                isRotation = false;
            }
        }
        public override void OnUpdate()
        {
            if (isRotation)
            {
                Quaternion tr = Quaternion.Slerp(roleTransform.rotation, _targetRotation, MoveController.Delta * MoveController.rotateSpeed * 0.5f);
                roleTransform.rotation = tr;

                float dot = Quaternion.Dot(_targetRotation, tr);
                float offest = 0.999999f;
                if (dot < 0)
                {
                    offest = -0.999999f;
                }
                if (dot > offest)
                {
                    roleTransform.rotation = _targetRotation;
                    isRotation = false;

                    Debug.LogWarning(Time.time + $"原地旋转End _targetRotation {_targetRotation}   tr {tr} ");
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
            targetRotation = Quaternion.Slerp(roleTransform.rotation, targetRotation, MoveController.Delta * MoveController.rotateSpeed * moveAmount);
            roleTransform.rotation = targetRotation;
        }
    }
}