using Core;
using UnityEngine;

namespace Core
{
    public class RotationBaseOnCameraOrientation : StateAction<FiniteStateMachinePlayer>
    {
        public MovementVariable variable;
        float t;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            if (variable.InputHorizontal == 0 || variable.InputVertical == 0)
            {
                return;
            }

            Vector3 targetDirection = fsm.CameraTransform.right * variable.InputHorizontal + fsm.CameraTransform.forward * variable.InputVertical;

            targetDirection.Normalize();
            targetDirection.y = 0;

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