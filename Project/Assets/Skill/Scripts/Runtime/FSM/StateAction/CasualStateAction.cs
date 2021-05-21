using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Skill
{
    [UnityEngine.CreateAssetMenu(menuName = "StateAction/Create CasualStateAction")]
    public class CasualStateAction : StateAction
    {
        private float curWaitTime;

        public int[] IdlesAnimatorClipHashs;
        public int[] IdlesAnimatorClipTimes;

        public float WaitTime;

        public FloatVariables DeltaTimeVariables;
        public Animator Animator;

        public override void Execute()
        {
            curWaitTime += DeltaTimeVariables.Value;

            if (curWaitTime >= WaitTime)
            {
                curWaitTime -= WaitTime - 2.93f;
                Animator.Play(Animator.StringToHash("CaualIdle_1"));
            }
        }
    }
}