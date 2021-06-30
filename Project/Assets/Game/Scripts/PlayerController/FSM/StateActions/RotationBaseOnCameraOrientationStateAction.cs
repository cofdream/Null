using Game.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class RotationBaseOnCameraOrientationStateAction : StateActionOld
    {
        [SerializeReference] public MovementVariable MovementVariables;
        [SerializeReference] public Transform Transform;
        [SerializeReference] public Transform CameraTransform;

        public float speed = 8f;

        public override void OnUpdate()
        {
            float h = MovementVariables.Horizontal;
            float v = MovementVariables.Vertical;

            Vector3 targetDirection = CameraTransform.forward * v + CameraTransform.right * h;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = Transform.forward;
            }

            Quaternion tragetRotation = Quaternion.LookRotation(targetDirection);
            Transform.rotation = Quaternion.Slerp(Transform.rotation, tragetRotation, Time.deltaTime * MovementVariables.MoveAmount * speed);
        }

    }
}