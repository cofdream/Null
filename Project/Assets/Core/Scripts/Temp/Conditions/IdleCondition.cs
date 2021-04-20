using UnityEngine;

namespace Core
{
    public class IdleCondition : Condition<FiniteStateMachinePlayer>
    {
        public MovementVariable movementVariable;
        public override bool Update(FiniteStateMachinePlayer fsm)
        {
            return movementVariable.MovementValue == 0;
        }
    }
}