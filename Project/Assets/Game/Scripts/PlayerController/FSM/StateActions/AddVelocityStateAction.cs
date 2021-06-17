using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class AddVelocityStateAction : StateAction
    {
        public Rigidbody Rigidbody;
        public Vector3 Direction;
        public float Speed;

        public override void OnEnter()
        {
            Rigidbody.velocity += Direction * Speed;
        }
    }
}