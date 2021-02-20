using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        Animator animator;

        [SerializeField]
        float walkSpeed = 5f;
        [SerializeField]
        float backSpeed = 2.5f;
        [SerializeField]
        float runSpeed = 10f;
        [SerializeField]
        float rotateSpeed = 10f;

        private bool isIdele;


        PlayerInput inputActions;

        float z;

        State state;

        private void Awake()
        {
            inputActions = new PlayerInput();
        }
        void Start()
        {
            state = new IdleState() { animator = animator };
            state.Enter();
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
            float tempZ = inputActions.Player2.Move.ReadValue<float>() * 0.125f;
            if (tempZ == 0)
            {
                z = 0;
                //animator?.Play("Idle_Guard_AR");

                if (state is IdleState == false)
                {
                    state.Exit();

                    state = new IdleState() { animator = animator };
                    state.Enter();
                }
            }
            else
            {
                z += tempZ;
                if (z >= 1)
                {
                    z = 1;
                }
                else if (z <= -1)
                {
                    z = -1;
                }

                transform.position += transform.TransformDirection(new Vector3(0, 0, z) * Time.deltaTime * walkSpeed);

                //animator?.Play("WalkFront_Shoot_AR");

                if (state is WalkState == false)
                {
                    state.Exit();

                    state = new WalkState() { animator = animator };
                    state.Enter();
                }
            }


            //Vector2 distance = inputActions.Player.Move.ReadValue<Vector2>();

            //if (distance.y != 0)
            //{

            //    Debug.Log(distance.y);

            //    transform.position += transform.TransformDirection(new Vector3(0, 0, distance.y) * Time.deltaTime * walkSpeed);

            //    animator?.Play("WalkForwardBattle");
            //}
            //else
            //{
            //    animator?.Play("Idle_Battle");
            //}

        }

        private void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }
    }
}