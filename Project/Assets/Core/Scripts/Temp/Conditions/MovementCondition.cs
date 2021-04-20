using UnityEngine;

namespace Core
{
    public class MovementCondition : Condition<FiniteStateMachinePlayer>
    {
        public MovementVariable movementVariable;
        public override bool Update(FiniteStateMachinePlayer fsm)
        {
            if (movementVariable.MovementValue == 0)
            {
                return false;
            }

            Vector3 direction = Vector3.zero;
            direction.x = movementVariable.InputVertical > 0 ? 1 : -1;
            direction.z = movementVariable.InputHorizontal > 0 ? 1 : -1;

            return direction != fsm.Transform.forward;
        }
    }
}