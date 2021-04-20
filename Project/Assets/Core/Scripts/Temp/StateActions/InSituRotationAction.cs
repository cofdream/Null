using UnityEngine;

namespace Core
{
    public class InSituRotationAction : StateAction<FiniteStateMachinePlayer>
    {
        int aa;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            if (aa++ > 100)
            {
                aa = 0;
                fsm.CurrentState.BackLastState(fsm);
            }
            else
            {
                Debug.Log("原地转");
            }
        }
    }
}