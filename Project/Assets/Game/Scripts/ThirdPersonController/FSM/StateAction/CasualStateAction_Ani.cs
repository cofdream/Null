using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Game
{
    public class CasualStateAction_Ani : StateAction
    {
        public FloatVariables DeltaTimeVariables;
        public Animator Animator;
        public AnimatorHashes AnimatorHashes;

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
    }
}