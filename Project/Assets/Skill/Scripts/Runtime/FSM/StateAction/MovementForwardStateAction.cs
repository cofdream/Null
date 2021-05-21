using DA.Core.FSM;
using UnityEngine;

namespace Skill
{
    public class MovementForwardStateAction : StateAction
    {
        public MovementVarible MovementVarible;
        public Rigidbody Rigidbody;
        public Transform Transform;
        public override void Execute()
        {
            if (MovementVarible.MoveAmount > 0.1f)
            {
                Rigidbody.drag = 0;
            }
            else
            {
                Rigidbody.drag = 4;
            }

            Vector3 targetVelocity = Transform.forward * MovementVarible.MoveAmount * MovementVarible.MoveSpeed * 0.01f;

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            Rigidbody.velocity = targetVelocity;
        }
    }
}