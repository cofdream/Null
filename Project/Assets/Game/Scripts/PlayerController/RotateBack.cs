using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class RotateBack : State
    {
        public float turnSpeed;

        private bool isTransitionLocomotionState;
        public State LocomotionState;

        public float t;

        public override void OnEnter(PlayerController playerController)
        {
            Debug.Log("Enter RotaeBack");
        }
        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Animator.SetFloat(AnimatorHashes.MoveVerticalParameter, playerController.Movement.MoveAmount, 0.2f, playerController.DeltaTime);

            Quaternion tragetRotation = Quaternion.LookRotation(playerController.TargetDirection);

            t += playerController.DeltaTime * turnSpeed;
            playerController.transform.rotation = Quaternion.Lerp(playerController.transform.rotation, tragetRotation, t);

            if (t >= 1f)
            {
                t = 0;
                isTransitionLocomotionState = true;
            }
        }
        public override void OnFixedUpdate(PlayerController playerController)
        {

        }
        public override void OnExit(PlayerController playerController)
        {
            Debug.Log("Exit RotaeBack");
        }
        public override bool CheckTransition(PlayerController playerController, out State targetState)
        {
            if (isTransitionLocomotionState)
            {
                isTransitionLocomotionState = false;
                targetState = LocomotionState;
                return true;
            }
            targetState = null;
            return false;
        }
    }
}