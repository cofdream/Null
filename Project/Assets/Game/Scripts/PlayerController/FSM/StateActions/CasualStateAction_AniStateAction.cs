using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class CasualStateAction_AniStateAction : StateAction
    {
        [SerializeReference] public Animator Animator;

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
                curCasualTime += Time.deltaTime;

                if (curCasualTime >= casulaTime)
                {
                    curCasualTime = 0;
                    isCasual = false;

                    //Animator.SetBool(AnimatorHashes.capusal, false);
                }
            }
            else
            {
                curWaitTime += Time.deltaTime;

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