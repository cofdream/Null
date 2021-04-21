using UnityEngine;

namespace DevTool
{
    public class InSituRotationCondition : Condition<FiniteStateMachinePlayer>
    {
        public override bool Update(FiniteStateMachinePlayer fsm)
        {
            var movementVariable = fsm.MovementVariable;

            if (movementVariable.MovementValue == 0)
            {
                return false;
            }

            Vector3 direction = Vector3.zero;
            if (movementVariable.InputHorizontal != 0)
            {
                direction.x = movementVariable.InputHorizontal > 0 ? 1 : -1;
            }
            if (movementVariable.InputVertical != 0)
            {
                direction.z = movementVariable.InputVertical > 0 ? 1 : -1;
            }

            fsm.Direction = direction;

            return direction != fsm.Transform.forward;
        }
    }
}