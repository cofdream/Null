using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public enum StateIdType : int
    {
        None = 0,
        Idle,
        Move,
    }
    public enum TranslationIdleType : int
    {
        None = 0,
        Idle_To_Move,
        Move_To_Idle,
    }
    public class MoveController : MonoBehaviour
    {
        public float walkSpeed;
        public float rotateSpeed;
        public float walkRotationSpeed;

        public float runSpeed;

        public PlayerInput inputActions;

        public new Rigidbody rigidbody;
        public Animator animator;

        public Transform model;
        public Transform cameraTransform;

        public float Delta { get { return Time.deltaTime; } }

        public FSM FSM { get; private set; }

        private void Awake()
        {
            inputActions = new PlayerInput();

            FSM = new FSM();

            var stateIdle = new StateIdle()
            {
                StateId = (int)StateIdType.Idle,
                MoveController = this,
            };
            var stateMove = new StateMove()
            {
                StateId = (int)StateIdType.Move,
                MoveController = this,
            };


            var fsmTranslationIdleToMove = new Conditions() { TranslationId = (int)TranslationIdleType.Idle_To_Move, FromState = stateIdle, ToState = stateMove, };
            var fsmTranslationMoveToIdle = new Conditions() { TranslationId = (int)TranslationIdleType.Move_To_Idle, FromState = stateMove, ToState = stateIdle, };

            FSM.Add(stateIdle);
            FSM.Add(stateMove);

            FSM.Add(fsmTranslationIdleToMove);
            FSM.Add(fsmTranslationMoveToIdle);

            FSM.Start(stateIdle);
        }
        private void OnEnable()
        {
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }

        void Update()
        {
            FSM.Update();
        }
    }
}