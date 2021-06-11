using Game.FSM;
using Game.Variable;
using Game.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class CasualStateAction_AniStateAction : StateAction
    {
        public FloatVariables DeltaTimeVariables;
        public ObjectVariable AnimatorVariable;
        public ObjectVariable AnimatorHashesVariable;

        private Animator Animator;
        private AnimatorHashes AnimatorHashes;

        public float WaitTime;
        private float curWaitTime;
        private float casulaTime;
        private float curCasualTime;
        private bool isCasual;

        public override void OnEnter()
        {
            isCasual = false;
            casulaTime = 2.5f;
            curWaitTime = 0f;
            curCasualTime = 0f;

            Animator = AnimatorVariable.Value as Animator;
            AnimatorHashes = AnimatorHashesVariable.Value as AnimatorHashes;
        }


        public override void OnUpdate()
        {
            if (isCasual)
            {
                curCasualTime += DeltaTimeVariables.Value;

                if (curCasualTime >= casulaTime)
                {
                    curCasualTime = 0;
                    isCasual = false;

                    //Animator.SetBool(AnimatorHashes.capusal, false);
                }
            }
            else
            {
                curWaitTime += DeltaTimeVariables.Value;

                if (curWaitTime >= WaitTime)
                {
                    curWaitTime = 0;
                    isCasual = true;

                    //Animator.SetBool(AnimatorHashes.capusal, true);
                }
            }
        }

        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            DeltaTimeVariables = GetCloneInstance(allDependencies, DeltaTimeVariables);
            AnimatorVariable = GetCloneInstance(allDependencies, AnimatorVariable);
            AnimatorHashesVariable = GetCloneInstance(allDependencies, AnimatorHashesVariable);
        }
    }
}