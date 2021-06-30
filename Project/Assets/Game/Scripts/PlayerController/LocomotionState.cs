using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LocomotionState : State
    {
        public float JumpSpeed;


        public override void OnEnter(PlayerController playerController)
        {

        }
        public override void OnUpdate(PlayerController playerController)
        {
            Movement(playerController);
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }

        private void Movement(PlayerController playerController)
        {
            if (playerController.Movement.MoveAmount > 0.1f)
            {
                playerController.Rigidbody.drag = 0;
            }
            else
            {
                playerController.Rigidbody.drag = 4;
            }

            Vector3 targetVelocity = playerController.Movement.MoveAmount * playerController.Movement.MoveSpeed * playerController.ModeTransform.forward;

            //if (states.isGrounded)
            //{
            //    targetVelocity.y = 0;
            //}
            //else
            //{
            //    targetVelocity.y = states.rigidbody.velocity.y;
            //}

            playerController.Rigidbody.velocity = targetVelocity;
        }
    }
}