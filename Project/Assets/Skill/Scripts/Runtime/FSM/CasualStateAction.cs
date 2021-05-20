using DA.Core.FSM;
using DA.Core.FSM.Variables;
using UnityEngine;

namespace Skill
{
    [UnityEngine.CreateAssetMenu(menuName = "StateAction/Create CasualStateAction")]
    public class CasualStateAction : StateAction
    {
        public float WaitTime;
        private float curWaitTime;
        public FloatVariables deltaTimeVariables;
        public override void Execute()
        {
            curWaitTime += deltaTimeVariables.Value;
        }
    }
}