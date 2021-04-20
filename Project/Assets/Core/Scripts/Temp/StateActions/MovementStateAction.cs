using Core;
using System;
using UnityEngine;

namespace Temp2
{
    [System.Serializable]
    public class MovementStateAction : StateAction<FiniteStateMachinePlayer>
    {
        public MovementVariable variable;

        private float drag = 4;
        private float movementSpeed = 2;

        private float movementLastValue;

        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            Debug.Log("Update MovementStateAction");
            if (Mathf.Approximately(variable.MovementValue, 0) && Mathf.Approximately(movementLastValue, variable.MovementValue))
            {
                fsm.CurrentState.BackLastState(fsm);
                return;
            }
            if (Mathf.Approximately(variable.MovementValue, 0))
            {
                fsm.Rigidbody.drag = 0;
            }
            else
            {
                fsm.Rigidbody.drag = drag;
            }

            Vector3 targetVelocity = fsm.Transform.forward * variable.MovementValue * movementSpeed;

            fsm.Rigidbody.velocity = targetVelocity;

            movementLastValue = variable.MovementValue;
        }
    }
}