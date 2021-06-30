using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class IdleState : State
    {
        public float JumpSpeed;

        public override void OnEnter(PlayerController playerController)
        {

        }
        public override void OnUpdate(PlayerController playerController)
        {
            if (playerController.IsJump)
            {
                playerController.IsJump = false;

                playerController.Rigidbody.velocity += JumpSpeed * Vector3.up;

                playerController.Animator.CrossFade(AnimatorHashes.JumpIdleState, 0.2f);
            }
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }
    }
}