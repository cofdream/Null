using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeReference]
        public PlayFiniteStateMachine PlayFiniteStateMachine;

        public bool IsJump;
        public bool IsRun;
        public Movement Movement;
        public Transform ModeTransform;
        public float DeltaTime;
        public Vector3 TargetDirection;

        public Animator Animator;
        public Rigidbody Rigidbody;
        public ControllerHangPoint ControllerHangPoint;
        public CameraHangPoint CameraHangPoint;


        private float maxValue;

        public float scale = 1;

        public void Start()
        {
            Movement = new Movement();
            PlayFiniteStateMachine.OnStart(this);

           
        }

        public void Update()
        {
            DeltaTime = Time.deltaTime;
            GetInputValues();

            PlayFiniteStateMachine.OnUpdate(this);
        }
        public void FixedUpdate()
        {
            PlayFiniteStateMachine.OnFixedUpdate(this);
        }



        private void GetInputValues()
        {
            Time.timeScale = scale;

            IsRun = Input.GetAxis("Fire3") == 1;
            IsJump = Input.GetButtonDown("Jump");

            Movement.Horizontal = Input.GetAxis("Horizontal");
            Movement.Vertical = Input.GetAxis("Vertical");

            // ¹ý¶É
            if (IsRun)
                maxValue = Mathf.Lerp(maxValue, 1f, Time.deltaTime * 6);
            else
                maxValue = 0.25f;//Mathf.Lerp(maxValue, 0.25f, Time.deltaTime * 6);

            //maxValue = 1;
            //v +1 -1
            var moveAmount = Mathf.Clamp(Mathf.Abs(Movement.Horizontal) + Mathf.Abs(Movement.Vertical), 0, maxValue);
            Movement.MoveAmount = moveAmount;
        }
    }
}