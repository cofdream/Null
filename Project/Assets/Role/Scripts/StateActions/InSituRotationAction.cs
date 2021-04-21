using UnityEngine;

namespace Core
{
    public class InSituRotationAction : StateAction<FiniteStateMachinePlayer>
    {
        public Vector3 direction;
        public float rotationSpeed = 8;
        private float t;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            Debug.Log("InSituRotation Update");

            //Vector3 targetDir = fsm.CameraTransform.forward * fsm.MovementVariable.InputHorizontal + fsm.CameraTransform.right * fsm.MovementVariable.InputVertical;
            Vector3 targetDir = fsm.Direction;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = fsm.Transform.forward;
            }

            var targetRotation = Quaternion.LookRotation(targetDir);

            t += fsm.deltaTime * rotationSpeed;

            fsm.Transform.rotation = Quaternion.Slerp(fsm.Transform.rotation, targetRotation, t);

            if (t >= 1)
            {
                t = 0;
                FiniteStateMachine<FiniteStateMachinePlayer>.EnterLastState(fsm);
                Debug.Log("InSituRotation Exit");
            }
        }
    }
}