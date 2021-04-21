using Core;
using UnityEngine;

namespace Core
{
    public class RotationBaseOnCameraOrientation : StateAction<FiniteStateMachinePlayer>
    {
        float t;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            var movementVariable = fsm.MovementVariable;

            Vector3 targetDirection = fsm.CameraTransform.right * movementVariable.InputHorizontal + fsm.CameraTransform.forward * movementVariable.InputVertical;

            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = fsm.Transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            t += Time.deltaTime * 8;

            fsm.Transform.rotation = Quaternion.Slerp(fsm.Transform.rotation, targetRotation, t);

            if (t >= 1)
            {
                t = 0;
            }
        }
    }
}