using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static RPG.StateRotation;

namespace RPG
{
    public class MoveController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float backSpeed = 2.5f;
        public float runSpeed = 10f;
        public float rotateSpeed = 10f;

        PlayerInput inputActions;


        public new Rigidbody rigidbody;

        public Transform model;
        public Transform cameraTransform;
        public float cameraSpeed = 8;


        FSM fsm;
        private void Awake()
        {
            inputActions = new PlayerInput();

            fsm = new FSM();

            ushort stateId = 0;

            FSM.State stateIdle = new FSM.State()
            {
                StateId = stateId++,
                Enter = StateIdle_OnEnter,
                Update = StateIdle_OnUpdate,
            };

            FSM.State stateMove = new FSM.State()
            {
                StateId = stateId++,
                Enter = StateMove_OnEnter,
                Update = StateMove_OnUpdate,
            };


            FSM.State stateRotation = new FSM.State()
            {
                StateId = stateId++,
                Enter = StateRotation_OnEnter,
                Update = StateRotation_OnUpdate,
            };


            ushort trabslationId = 0;
            FSM.FSMTranslation fsmTranslationIdleToMove = new FSM.FSMTranslation() { TranslationId = trabslationId++, FromState = stateIdle, ToState = stateMove, };

            FSM.FSMTranslation fsmTranslationMoveToIdle = new FSM.FSMTranslation() { TranslationId = trabslationId++, FromState = stateMove, ToState = stateIdle, };

            FSM.FSMTranslation fsmTranslationMoveToRotation = new FSM.FSMTranslation() { TranslationId = trabslationId++, FromState = stateMove, ToState = stateRotation, };

            FSM.FSMTranslation fsmTranslationRotationToIdle = new FSM.FSMTranslation() { TranslationId = trabslationId++, FromState = stateRotation, ToState = stateIdle, };

            fsm.Add(stateIdle);
            fsm.Add(stateMove);
            fsm.Add(stateRotation);

            fsm.Add(fsmTranslationIdleToMove);
            fsm.Add(fsmTranslationMoveToIdle);
            fsm.Add(fsmTranslationMoveToRotation);
            fsm.Add(fsmTranslationRotationToIdle);


            fsm.Start(stateIdle);
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
            fsm.Update();

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

        float horiazontal;
        float vertical;
        Quaternion targetQuaternion;

        RotationType rotationType;
        

        private void StateIdle_OnEnter()
        {
            rigidbody.drag = 0;
            Debug.Log("Idle State Enter");
        }
        private void StateIdle_OnUpdate()
        {
            Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();

            if (move.x != 0 || move.y != 0)
            {
                fsm.HandleEvent(0);
            }
        }

        private void StateMove_OnEnter()
        {
            rigidbody.drag = 4;
            Debug.Log("Move State Enter");
        }
        private void StateMove_OnUpdate()
        {
            Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();

            if (move.x == 0 && move.y == 0)
            {
                fsm.HandleEvent(1);
                return;
            }

            horiazontal = move.y;
            vertical = move.x;


            float up = 0;
            float back = 0;// not use
            float left = 0;// not use
            float right = 0;

            //float offset = cameraTransform.eulerAngles.y;

            //Debug.Log("Offset " + offset);
            // 朝前
            if ((int)transform.eulerAngles.y == 0)
            {
                up = horiazontal;
                back = -horiazontal;
                left = -vertical;
                right = vertical;
            }
            // 朝brak
            else if ((int)transform.eulerAngles.y == 180)
            {
                up = -horiazontal;
                back = horiazontal;
                left = vertical;
                right = -vertical;
            }
            // 朝 Right
            else if ((int)transform.eulerAngles.y == 90)
            {
                up = vertical;
                back = -vertical;
                left = horiazontal;
                right = -horiazontal;
            }
            // 朝 Lefg
            else if ((int)transform.eulerAngles.y == 270)
            {
                up = -vertical;
                back = vertical;
                left = -horiazontal;
                right = horiazontal;
            }
            else
            {
                Debug.LogError($"{transform.forward}  not forward");
            }


            if (up >= 1)
            {
                float moveAmount = Mathf.Clamp01(Mathf.Abs(horiazontal) + Mathf.Abs(vertical));

                Vector3 targetVelocity = transform.forward * moveAmount * walkSpeed;
                targetVelocity.y = rigidbody.velocity.y;
                rigidbody.velocity = targetVelocity;
            }
            else
            {
                if (up == 0)
                {
                    if (right > 0)
                    {
                        rotationType = RotationType.Right;
                        Debug.Log("Rotation Right");
                    }
                    else if (right < 0)
                    {
                        rotationType = RotationType.Left;
                        Debug.Log("Rotation Left");
                    }
                    else
                    {
                        Debug.LogError("Rotation");
                    }
                }
                else
                {
                    rotationType = RotationType.Back;
                    Debug.Log("Rotation Back");
                }

                fsm.HandleEvent(2);
            }
        }


        float time = 0;
        private void StateRotation_OnEnter()
        {

            switch (rotationType)
            {
                case RotationType.Back:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 180, 0)) * transform.rotation;
                    break;
                case RotationType.Left:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, -90, 0)) * transform.rotation;
                    break;
                case RotationType.Right:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 90, 0)) * transform.rotation;
                    break;
                default:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 0, 0)) * transform.rotation;
                    break;
            }

            Debug.LogWarning((transform.rotation * targetQuaternion).eulerAngles);

            Debug.Log("Rotation State Enter");
        }
        private void StateRotation_OnUpdate()
        {
            time += Time.deltaTime * cameraSpeed;
            if (time >= 1)
            {
                time = 0;

                transform.rotation = targetQuaternion;
                fsm.HandleEvent(3);

                Debug.Log("Rotation end...");
            }
            else
            {
                // 需要反方向旋转，交换参数a，b的位置
                Quaternion targetRotation = Quaternion.Lerp(transform.rotation, targetQuaternion, time);
                transform.rotation = targetRotation;

                Debug.Log("Rotationing...");
            }
        }
    }
}