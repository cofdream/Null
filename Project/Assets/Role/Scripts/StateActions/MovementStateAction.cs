using Core;
using System;
using UnityEngine;

namespace Temp2
{
    [System.Serializable]
    public class MovementStateAction : StateAction<FiniteStateMachinePlayer>
    {
        private float drag = 4;
        private float movementSpeed = 2;

        private float movementLastValue;

        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            //fsm.MovementVariable.UpdateVariable();

            Debug.Log("Update Movement");

            var movementVariable = fsm.MovementVariable;

            if (Mathf.Approximately(movementVariable.MovementValue, 0) && Mathf.Approximately(movementLastValue, movementVariable.MovementValue))
            {
                FiniteStateMachine<FiniteStateMachinePlayer>.EnterLastState(fsm);
                Debug.Log("Exit Movement");
                return;
            }
            if (Mathf.Approximately(movementVariable.MovementValue, 0))
            {
                fsm.Rigidbody.drag = 0;
            }
            else
            {
                fsm.Rigidbody.drag = drag;
            }

            Vector3 targetVelocity = fsm.Transform.forward * movementVariable.MovementValue * movementSpeed;

            fsm.Rigidbody.velocity = targetVelocity;

            movementLastValue = movementVariable.MovementValue;
        }
    }
}