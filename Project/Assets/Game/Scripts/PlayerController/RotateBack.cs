using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class RotateBack : State
    {
        public float turnSpeed;

        public State LocomotionState;

        public float t;
        public Quaternion lastRotation;
        public override void OnEnter(PlayerController playerController)
        {
            Debug.Log("Enter RotaeBack");

            lastRotation = playerController.transform.rotation;

            // z 前后，x左右
            playerController.Animator.SetBool(AnimatorHashes.QuickTurnLeft,
                playerController.TargetDirection.z != 0
                ? playerController.TargetDirection.z < 0
                : playerController.TargetDirection.x < 0);
        }
        public override void OnUpdate(PlayerController playerController)
        {
            t += playerController.DeltaTime * turnSpeed;

            Quaternion tragetRotation = Quaternion.LookRotation(playerController.TargetDirection);

            playerController.transform.rotation = Quaternion.Lerp(lastRotation, tragetRotation, t);

            if (t >= 1f)
            {
                t = 0;
                playerController.TransitionState(LocomotionState);
                return;
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