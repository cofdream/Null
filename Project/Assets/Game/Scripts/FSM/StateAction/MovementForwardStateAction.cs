using Game.FSM;
using Game.Variable;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MovementForwardStateAction : StateAction
    {
        [SerializeField] private MovementVariables  MovementVariables;
        [SerializeField] private ObjectVariable     RigidbodyVariables;
        [SerializeField] private GameobjectVariable TargetGOVariables;


        private Rigidbody rigidbody;
        private Transform transform;

        public override void OnEnter()
        {
            rigidbody = RigidbodyVariables.Value as Rigidbody;
            transform = TargetGOVariables.Value.transform;
        }

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

            Vector3 targetVelocity = 0.01f * MovementVariables.MoveAmount * MovementVariables.MoveSpeed * transform.forward;

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
        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            MovementVariables = GetCloneInstance(allDependencies, MovementVariables);
            RigidbodyVariables = GetCloneInstance(allDependencies, RigidbodyVariables);
            TargetGOVariables = GetCloneInstance(allDependencies, TargetGOVariables);
        }
    }
}