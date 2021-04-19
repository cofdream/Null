using Core;
using System;
using UnityEngine;

namespace Temp2
{
    [System.Serializable]
    public class MovementState : StateAction<FiniteStateMachinePlayer>
    {
        [SerializeField] float movementValue;
        [SerializeField] float drag = 8;

        public override void Execute(FiniteStateMachinePlayer fsm)
        {
            float axisH = Input.GetAxis("Horizontal");
            float axisV = Input.GetAxis("Vertical");

            movementValue = Mathf.Clamp01(Mathf.Abs(axisH) + Mathf.Abs(axisV));

            if (movementValue == 0)
            {
                fsm.Rigidbody.drag = 0;
            }
            else
            {
                fsm.Rigidbody.drag = drag;
            }
            fsm.Rigidbody.SetDensity(movementValue);
        }
    }
}