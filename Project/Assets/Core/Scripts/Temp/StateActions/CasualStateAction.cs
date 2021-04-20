using UnityEngine;

namespace Core
{
    public class CasualStateAction : StateAction<FiniteStateMachinePlayer>
    {
        public CasualVariable variable;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            variable.time += fsm.deltaTime;

            if (variable.time > variable.ChangTime)
            {
                variable.time -= variable.ChangTime;

                switch (Random.Range(0, 3))
                {
                    case 0: Debug.Log("InSituRotation 00000"); break;
                    case 1: Debug.Log("InSituRotation 11111"); break;
                    case 2: Debug.Log("InSituRotation 22222"); break;
                    default:
                        break;
                };
            }
        }
    }
}