using Game.FSM;
using Game.Variables;
using UnityEngine;

namespace Game
{
    public class FollowTransformStateAction : StateAction
    {
        public Transform TargetTransform;
        public Transform CurrentTransform;

        public FloatVariables DeltaVariables;

        public float speed = 9;
        public override void OnUpdate()
        {
            Vector3 targetPosition = Vector3.Lerp(CurrentTransform.position, TargetTransform.position, DeltaVariables.Value * speed);

            CurrentTransform.position = targetPosition;
        }
    }
}