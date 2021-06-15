using Game.FSM;
using Game.Variable;
using Game.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MovementForward_AniStateAction : StateAction
    {
        [SerializeReference] public MovementVariables MovementVariables;
        [SerializeReference] public Transform Transform;
        [SerializeReference] public Animator Animator;
        [SerializeReference] public AnimatorHashes AnimatorHashes;
        [SerializeReference] public AnimatorData AnimatorData;

        public override void OnEnter()
        {
            AnimatorData = new AnimatorData(Animator);
            AnimatorHashes = new AnimatorHashes();
        }
        public override void OnUpdate()
        {
            Animator.SetFloat(AnimatorHashes.Vertical, MovementVariables.MoveAmount, 0.2f, Time.deltaTime);
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