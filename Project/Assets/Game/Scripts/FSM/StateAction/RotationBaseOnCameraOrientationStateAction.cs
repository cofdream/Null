using Game.FSM;
using Game.Variable;
using Game.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class RotationBaseOnCameraOrientationStateAction : StateAction
    {
        [SerializeField] private FloatVariables DeltaVariables;
        [SerializeField] private MovementVariables MovementVariables;
        [SerializeField] private GameobjectVariable TransformVariables;
        [SerializeField] private GameobjectVariable CameraTransformVariables;


        [SerializeField] private float speed = 8f;


        private Transform Transform;
        private Transform CameraTransform;

        public override void OnEnter()
        {
            Transform = TransformVariables.Value.transform;
            CameraTransform = CameraTransformVariables.Value.transform;
        }

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

        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            DeltaVariables = GetCloneInstance(allDependencies, DeltaVariables);
            MovementVariables = GetCloneInstance(allDependencies, MovementVariables);
            TransformVariables = GetCloneInstance(allDependencies, TransformVariables);
            CameraTransformVariables = GetCloneInstance(allDependencies, CameraTransformVariables);
        }
    }
}