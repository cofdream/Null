using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static RPG.StateRotation;

namespace RPG
{
    public enum StateIdType : int
    {
        None = 0,
        Idle,
        Move,
        Rotation,
    }
    public enum TranslationIdleType : int
    {
        None = 0,
        Idle_To_Move,
        Move_To_Idle,
        Move_To_Rotation,
        Rotation_To_Idle,
    }
    public class MoveController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float backSpeed = 2.5f;
        public float runSpeed = 10f;
        public float rotateSpeed = 10f;

        public PlayerInput inputActions;

        public new Rigidbody rigidbody;

        public Transform model;
        public Transform cameraTransform;
        public float cameraSpeed = 8;

        public FSM FSM { get; private set; }

        [HideInInspector] public RotationType rotationType;

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

            var stateRotation = new StateRotation()
            {
                StateId = (int)StateIdType.Rotation,
                MoveController = this,
            };

            var fsmTranslationIdleToMove = new FSM.FSMTranslation() { TranslationId = (int)TranslationIdleType.Idle_To_Move, FromState = stateIdle, ToState = stateMove, };
            var fsmTranslationMoveToIdle = new FSM.FSMTranslation() { TranslationId = (int)TranslationIdleType.Move_To_Idle, FromState = stateMove, ToState = stateIdle, };
            var fsmTranslationMoveToRotation = new FSM.FSMTranslation() { TranslationId = (int)TranslationIdleType.Move_To_Rotation, FromState = stateMove, ToState = stateRotation, };
            var fsmTranslationRotationToIdle = new FSM.FSMTranslation() { TranslationId = (int)TranslationIdleType.Rotation_To_Idle, FromState = stateRotation, ToState = stateIdle, };

            FSM.Add(stateIdle);
            FSM.Add(stateMove);
            FSM.Add(stateRotation);

            FSM.Add(fsmTranslationIdleToMove);
            FSM.Add(fsmTranslationMoveToIdle);
            FSM.Add(fsmTranslationMoveToRotation);
            FSM.Add(fsmTranslationRotationToIdle);


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

            //else if (move.x != 0)
            //{
            //    Vector3 targetDir = transform.forward * move.y;
            //    targetDir += transform.right * move.x;
            //    targetDir.Normalize();
            //    targetDir.y = 0;

            //    if (targetDir == Vector3.zero)
            //    {
            //        targetDir = transform.forward;
            //    }

            //    Quaternion tr = Quaternion.LookRotation(targetDir);
            //    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * moveAmount * cameraSpeed);

            //    transform.rotation = targetRotation;
            //}
        }
    }
}