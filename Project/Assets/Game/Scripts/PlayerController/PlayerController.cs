using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeReference] public PlayFiniteStateMachine PlayFiniteStateMachine;

        public bool IsJump;
        public bool IsRun;
        public Movement Movement;
        public Transform ModeTransform;
        public float DeltaTime;
        public Vector3 TargetDirection;
        public bool IsGround;

        public Animator Animator;
        public Rigidbody Rigidbody;
        public ControllerHangPoint ControllerHangPoint;
        public CameraHangPoint CameraHangPoint;


        public float scale = 1;


        public void Start()
        {
            Movement = new Movement();
            TargetDirection = transform.forward;

            PlayFiniteStateMachine.OnStart(this);
        }

        public void Update()
        {
            DeltaTime = Time.deltaTime;
            Time.timeScale = scale;

            GetInputValues();

            PlayFiniteStateMachine.OnUpdate(this);
        }
        public void FixedUpdate()
        {
            PlayFiniteStateMachine.OnFixedUpdate(this);
        }



        private void GetInputValues()
        {
            IsRun = Input.GetAxis("Fire3") == 1;
            IsJump = Input.GetButtonDown("Jump");

            Movement.Horizontal = Input.GetAxis("Horizontal");
            Movement.Vertical = Input.GetAxis("Vertical");

            Movement.MoveAmount = Mathf.Clamp(Mathf.Abs(Movement.Horizontal) + Mathf.Abs(Movement.Vertical), 0, IsRun ? 1f : 0.1f);
        }

        public void TransitionState(State targetState, bool isUpdate = false)
        {
            PlayFiniteStateMachine.TransitionState(this, targetState, isUpdate);
        }

        public void EntWaitState(float waitTime, State endState, bool isUpdate = false)
        {
            PlayFiniteStateMachine.Wait(this, waitTime, endState, isUpdate);
        }
    }
}