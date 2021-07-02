using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class OnAirState : State
    {

        public override void OnEnter(PlayerController playerController)
        {

        }
        public override void OnUpdate(PlayerController playerController)
        {
            var transform = playerController.transform;
            Vector3 origin = transform.position;
            origin.y += 0.7f;

            Vector3 direction = -transform.up;

            float distance = 1.4f;

            if (Physics.Raycast(origin, direction, out RaycastHit raycastHit, distance))
            {
                playerController.IsGround = true;
            }
            else
            {
                playerController.IsGround = false;
            }

            Debug.DrawRay(origin, direction, Color.red, distance);
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {

        }
    }
}