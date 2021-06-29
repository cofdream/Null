using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class JumpStateAction : StateAction
    {
        [SerializeReference] public RigidbodyVariable Rigidbody;
        [SerializeReference] public BoolVariable IsJump;
        [SerializeReference] public BoolVariable IsRun;
        [SerializeReference] public BoolVariable IsGround;
        public Vector3 Direction;

        public float WalkSpeed;
        public float RunSpeed;
        public override void OnEnter()
        {
            if (IsGround.Value == false)
            {
                Debug.Log("IsGround 的值异常");
            }
            IsGround.Value = true;
        }
        public override void OnFixedUpdate()
        {
            if (IsJump.Value == true && IsGround.Value == true)
            {
                Rigidbody.Value.velocity += Direction * (IsRun ? RunSpeed : WalkSpeed);
                IsGround.Value = false;
            }
        }
    }
}