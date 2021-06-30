using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class JumpStateAction : StateActionOld
    {
        [SerializeReference] public RigidbodyVariable Rigidbody;
        [SerializeReference] public BoolVariable IsJump;
        [SerializeReference] public BoolVariable IsGround;
        [SerializeReference] public AnimatorVariable Animator;
        public bool isIdle;
        public float Speed;

        public override void OnEnter()
        {
            if (IsGround.Value == false)
            {
                Debug.Log("IsGround 的值异常");
            }
            IsGround.Value = true;
        }

        public override void OnUpdate()
        {
            if (IsJump.Value)
            {
                IsJump.Value = false;
                IsGround.Value = false;
                Rigidbody.Value.velocity += Speed * Vector3.up;

                if (isIdle)
                {
                    Animator.Value.CrossFade(AnimatorHashes.JumpIdleState, 0.37f);
                }
                else
                {
                    Animator.Value.CrossFade(AnimatorHashes.JumpRunState, 0.2f);
                }
            }
        }
    }
}