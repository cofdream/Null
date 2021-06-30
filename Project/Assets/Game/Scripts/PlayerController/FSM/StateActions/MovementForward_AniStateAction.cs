using Game.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MovementForward_AniStateAction : StateActionOld
    {
        [SerializeReference] public MovementVariable MovementVariables;
        [SerializeReference] public TransformVariable Transform;
        [SerializeReference] public AnimatorVariable Animator;
        [NonSerialized] private AnimatorData AnimatorData;

        public override void OnEnter()
        {
            if (AnimatorData == null)
                AnimatorData = new AnimatorData(Animator.Value);
        }
        public override void OnUpdate()
        {
            Animator.Value.SetFloat(AnimatorHashes.MoveVerticalParameter, MovementVariables.MoveAmount, 0.2f, Time.deltaTime);
        }
        public override void OnExit()
        {
            Animator.Value.SetFloat(AnimatorHashes.MoveVerticalParameter, 0);


            Vector3 lf_relative = Transform.Value.InverseTransformPoint(AnimatorData.leftFoot.position);
            Vector3 rf_relative = Transform.Value.InverseTransformPoint(AnimatorData.rightFoot.position);

            bool leftForward = false;
            if (lf_relative.z > rf_relative.z)
            {
                leftForward = true;
            }
            Animator.Value.SetBool(AnimatorHashes.LeftFootForwardParameter, !leftForward);
        }
    }
}