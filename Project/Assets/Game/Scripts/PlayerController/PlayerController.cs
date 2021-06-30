using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public Animator Animator;
        public Rigidbody Rigidbody;
        public ControllerHangPoint ControllerHangPoint;

        public PlayFiniteStateMachine PlayFiniteStateMachine;

        public bool IsJump;
        public bool IsRun;
        public Movement Movement;
        public Transform ModeTransform;

        public void Start()
        {
            PlayFiniteStateMachine.CurrentState = PlayFiniteStateMachine.AllStates[0];
            PlayFiniteStateMachine.Start(this);
        }

        public void Update()
        {
            GetInputValues();

            PlayFiniteStateMachine.Update(this);
        }
        public void FixedUpdate()
        {
            PlayFiniteStateMachine.FixedUpdate(this);
        }

        private void GetInputValues()
        {
            IsRun = Input.GetAxis("Fire3") == 1;
            IsJump = Input.GetButtonDown("Jump");

            Movement.Horizontal = Input.GetAxis("Horizontal");
            Movement.Vertical = Input.GetAxis("Vertical");

            float maxValue;
            if (IsRun)
                maxValue = 1;
            else
                maxValue = 0.25f;

            var moveAmount = Mathf.Clamp(Mathf.Abs(Movement.Horizontal) + Mathf.Abs(Movement.Vertical), 0, maxValue);
            Movement.MoveAmount = moveAmount;

            //IsMovement.Value = moveAmount > 0.1f;
        }
    }
}