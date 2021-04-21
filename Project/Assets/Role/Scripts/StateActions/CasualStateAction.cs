using UnityEngine;

namespace DevTool
{
    public class CasualStateAction : StateAction<FiniteStateMachinePlayer>
    {
        public float time;
        public float ChangTime = 60f;

        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            //fsm.MovementVariable.UpdateVariable();

            time += fsm.deltaTime;

            if (time > ChangTime)
            {
                time -= ChangTime;

                switch (Random.Range(0, 3))
                {
                    case 0: Debug.Log("InSituRotation 00000"); break;
                    case 1: Debug.Log("InSituRotation 11111"); break;
                    case 2: Debug.Log("InSituRotation 22222"); break;
                    default:
                        break;
                };
            }

            Debug.Log("Idle Update");
        }
    }
}