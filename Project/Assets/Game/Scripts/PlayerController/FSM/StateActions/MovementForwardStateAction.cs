using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MovementForwardStateAction : StateActionOld
    {
        [SerializeReference] public MovementVariable MovementVariables;
        [SerializeReference] public RigidbodyVariable Rigidbody;
        [SerializeReference] public TransformVariable Transform;

        public override void OnFixedUpdate()
        {
            if (MovementVariables.MoveAmount > 0.1f)
            {
                Rigidbody.Value.drag = 0;
            }
            else
            {
                Rigidbody.Value.drag = 4;
            }

            Vector3 targetVelocity = MovementVariables.MoveAmount * MovementVariables.MoveSpeed * Transform.Value.forward;

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            Rigidbody.Value.velocity = targetVelocity;
        }
    }
}