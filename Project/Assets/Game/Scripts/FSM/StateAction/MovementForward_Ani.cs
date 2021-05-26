using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Game
{
    public class MovementForward_Ani : StateAction
    {
        public AnimatorHashes AnimatorHashes;
        public Transform Transform;
        public Animator Animator;
        public AnimatorData AnimatorData;
        public MovementVarible MovementVarible;
        public FloatVariables DeltaTimeVariables;
        public override void OnUpdate()
        {
            Animator.SetFloat(AnimatorHashes.Vertical, MovementVarible.MoveAmount, 0.2f, DeltaTimeVariables.Value);
        }
        public override void OnExit()
        {
            Animator.SetFloat(AnimatorHashes.Vertical, 0);

            
            Vector3 lf_relative = Transform.InverseTransformPoint(AnimatorData.leftFoot.position);
            Vector3 rf_relative = Transform.InverseTransformPoint(AnimatorData.rightFoot.position);

            bool leftForward = false;
            if (lf_relative.z > rf_relative.z)
            {
                leftForward = true;
            }
            Animator.SetBool(AnimatorHashes.LeftFootForward, !leftForward);
        }
    }
}