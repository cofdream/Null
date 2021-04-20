using Core;
using System;
using UnityEngine;

namespace Temp2
{
    [System.Serializable]
    public class MovementStateAction : StateAction<FiniteStateMachinePlayer>
    {
        public MovementVariable variable;
        public override void Execute(FiniteStateMachinePlayer fsm)
        {

            if (Mathf.Approximately(variable.MovementValue, 0) && Mathf.Approximately(variable.MovementLastValue, variable.MovementValue))
            {
                Debug.Log("Stop Movement");
                return;
            }
            if (Mathf.Approximately(variable.MovementValue, 0))
            {
                fsm.Rigidbody.drag = 0;
            }
            else
            {
                fsm.Rigidbody.drag = variable.Drag;
            }

            Vector3 targetVelocity = fsm.Transform.forward * variable.MovementValue * variable.MovementSpeed;

            fsm.Rigidbody.velocity = targetVelocity;

            variable.MovementLastValue = variable.MovementValue;
        }
    }
}