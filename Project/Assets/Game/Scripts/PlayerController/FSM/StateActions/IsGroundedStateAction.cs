using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class IsGroundedStateAction : StateAction
    {
        [SerializeReference] public RigidbodyVariable Rigidbody;
        [SerializeReference] public TransformVariable Transform;
        [SerializeReference] public BoolVariable IsGround;
        //public int JumpNumber = 1;

        public override void OnFixedUpdate()
        {
            Vector3 origin = Transform.Value.position;
            origin.y += 0.7f;

            Vector3 direction = -Transform.Value.up;

            float distance = 1.4f;

            if (Physics.Raycast(origin, direction, out RaycastHit raycastHit, distance))
            {
                IsGround.Value = true;
            }
            else
            {
                IsGround.Value = false;
            }

            Debug.DrawRay(origin, direction, Color.red, distance);
        }
    }
}