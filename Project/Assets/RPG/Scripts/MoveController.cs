using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public Transform cameraTranform;
        public float cameraSpeed = 8;


        FSM fsm;

        float horiazontal;
        float vertical;
        float moveAmount;
        Vector3 targetDir;

        RotationType rotationType;
        enum RotationType
        {
            None = 0,
            Back,
            Left,
            Right,
        }


        private void Awake()
        {
            inputActions = new PlayerInput();

            fsm = new FSM();

            ushort stateId = 0;

            FSM.State stateIdle = new FSM.State()
            {
                StateId = stateId++,
                Enter = () => { rigidbody.drag = 0; Debug.Log("Idle State Enter"); },
                Update = () =>
                {
                    Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();

                    if (move.x != 0 || move.y != 0)
                    {
                        fsm.HandleEvent(0);
                    }
                }
            };

            FSM.State stateMove = new FSM.State()
            {
                StateId = stateId++,
                Enter = () => { rigidbody.drag = 4; Debug.Log("Move State Enter"); },
                Update = () =>
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
                    float back = 0;
                    float left = 0;
                    float right = 0;
                    // 朝前
                    if (transform.forward.z > 0.1f)
                    {
                        up = horiazontal;
                        back = -horiazontal;
                        left = -vertical;
                        right = vertical;
                    }
                    // 朝brak
                    else if (transform.forward.z < -0.1f)
                    {
                        up = -horiazontal;
                        back = horiazontal;
                        left = vertical;
                        right = -vertical;
                    }
                    else if (transform.forward.x > 0.1f)
                    {
                        up = vertical;
                        back = -vertical;
                        left = horiazontal;
                        right = -horiazontal;
                    }
                    else if (transform.forward.x < -0.1f)
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
                        moveAmount = Mathf.Clamp01(Mathf.Abs(horiazontal) + Mathf.Abs(vertical));

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
            };


            FSM.State stateRotation = new FSM.State()
            {
                StateId = stateId++,
                Enter = () =>
                {
                    rigidbody.drag = 0;

                    targetDir = transform.forward;
                    switch (rotationType)
                    {
                        case RotationType.Back:
                            targetDir = new Vector3(0, 180, 0);
                            break;
                        case RotationType.Left:
                            targetDir = new Vector3(0, -90, 0);
                            break;
                        case RotationType.Right:
                            targetDir = new Vector3(0, 90, 0);
                            break;
                    }

                    Debug.Log("Rotation State Enter");
                },
                Update = () =>
                {
                    moveAmount = Mathf.Clamp01(Mathf.Abs(horiazontal) + Mathf.Abs(vertical));

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetDir), Time.deltaTime * moveAmount * cameraSpeed);

                    transform.rotation = targetRotation;

                    if (Math.Abs(Math.Abs(targetRotation.eulerAngles.y) - Math.Abs(targetDir.y)) <= 1f)
                    {
                        transform.eulerAngles = targetDir;
                        fsm.HandleEvent(3);
                        Debug.Log("Rotation end..." + Time.time);
                    }
                    else
                    {
                        Debug.Log("Rotationing...");
                    }

                    //StartCoroutine(coroutine());
                }
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
        void Start()
        {

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


        private void OnValidate()
        {

        }
    }
}