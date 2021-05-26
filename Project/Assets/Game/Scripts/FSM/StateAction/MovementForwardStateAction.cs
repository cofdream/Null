using DA.Core.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MovementForwardStateAction : StateAction
    {
        public MovementVariables MovementVariables;
        public Rigidbody Rigidbody;
        public Transform Transform;
        public override void OnUpdate()
        {
            if (MovementVariables.MoveAmount > 0.1f)
            {
                Rigidbody.drag = 0;
            }
            else
            {
                Rigidbody.drag = 4;
            }

            Vector3 targetVelocity = Transform.forward * MovementVariables.MoveAmount * MovementVariables.MoveSpeed * 0.01f;

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