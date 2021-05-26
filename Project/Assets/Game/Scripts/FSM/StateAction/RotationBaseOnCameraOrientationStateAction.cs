using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class RotationBaseOnCameraOrientationStateAction : StateAction
    {
        public FloatVariables DeltaVariables;
        public MovementVariables MovementVariables;
        public Transform Transform;
        public Transform CameraTransform;
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
            Transform.rotation = Quaternion.Slerp(Transform.rotation, tragetRotation, DeltaVariables.Value * MovementVariables.MoveAmount * speed);
        }
    }
}