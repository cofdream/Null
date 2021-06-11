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
        [SerializeField] private ObjectVariable AnimatorHashesVariable;
        [SerializeField] private GameobjectVariable TransformVariable;
        [SerializeField] private MovementVariables MovementVariables;
        [SerializeField] private FloatVariables DeltaTimeVariables;
        [SerializeField] private ObjectVariable AnimatorVariables;

        private AnimatorHashes AnimatorHashes;
        private Transform Transform;
        private Animator Animator;

        private AnimatorData AnimatorData;

        public override void OnEnter()
        {
            AnimatorHashes = AnimatorHashesVariable.Value as AnimatorHashes;
            Transform = TransformVariable.Value.transform;
            Animator = AnimatorVariables.Value as Animator;

            AnimatorData = new AnimatorData(Animator);
        }
        public override void OnUpdate()
        {
            Animator.SetFloat(AnimatorHashes.Vertical, MovementVariables.MoveAmount, 0.2f, DeltaTimeVariables.Value);
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

        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            AnimatorHashesVariable = GetCloneInstance(allDependencies, AnimatorHashesVariable);
            TransformVariable = GetCloneInstance(allDependencies, TransformVariable);
            MovementVariables = GetCloneInstance(allDependencies, MovementVariables);
            DeltaTimeVariables = GetCloneInstance(allDependencies, DeltaTimeVariables);
        }
    }
}