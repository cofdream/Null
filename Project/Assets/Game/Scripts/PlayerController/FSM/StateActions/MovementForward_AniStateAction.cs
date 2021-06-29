using Game.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MovementForward_AniStateAction : StateAction
    {
        [SerializeReference] public MovementVariable MovementVariables;
        [SerializeReference] public TransformVariable Transform;
        [SerializeReference] public AnimatorVariable Animator;
        [SerializeReference] private AnimatorData AnimatorData;

        public override void OnEnter()
        {
            //if (AnimatorData == null)
                AnimatorData = new AnimatorData(Animator.Value);
        }
        public override void OnUpdate()
        {
            Animator.Value.SetFloat(AnimatorHashes.MoveVertical, MovementVariables.MoveAmount, 0.2f, Time.deltaTime);
        }
        public override void OnExit()
        {
            Animator.Value.SetFloat(AnimatorHashes.MoveVertical, 0);


            Vector3 lf_relative = Transform.Value.InverseTransformPoint(AnimatorData.leftFoot.position);
            Vector3 rf_relative = Transform.Value.InverseTransformPoint(AnimatorData.rightFoot.position);

            bool leftForward = false;
            if (lf_relative.z > rf_relative.z)
            {
                leftForward = true;
            }
            Animator.Value.SetBool(AnimatorHashes.LeftFootForward, !leftForward);
        }
    }
}