using Game.FSM;
using Game.Variable;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MovementForwardStateAction : StateAction
    {
        [SerializeReference] public MovementVariables MovementVariables;
        [SerializeReference] public Rigidbody rigidbody;
        [SerializeReference] public Transform transform;

        public override void OnUpdate()
        {
            if (MovementVariables.MoveAmount > 0.1f)
            {
                rigidbody.drag = 0;
            }
            else
            {
                rigidbody.drag = 4;
            }

            Vector3 targetVelocity =  MovementVariables.MoveAmount * MovementVariables.MoveSpeed * transform.forward;

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            rigidbody.velocity = targetVelocity;
        }
    }
}