using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class DogKnight : MonoBehaviour
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

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator?.Play("Attack01");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                animator?.Play("Attack02");

                
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    float z = Input.GetAxis("Vertical");
                    transform.Translate(new Vector3(0, 0, z) * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed));
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animator?.Play("RunForwardBattle");
                    }
                    else
                    {
                        animator?.Play("WalkForwardBattle");
                    }
                    isIdele = false;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    float z = Input.GetAxis("Vertical");
                    transform.Translate(new Vector3(0, 0, z) * Time.deltaTime * backSpeed);
                }

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    float x = Input.GetAxis("Horizontal");
                    transform.Rotate(new Vector3(0, x, 0) * Time.deltaTime * rotateSpeed);
                }
            }

            

            if (isIdele == false&& Input.GetKey(KeyCode.W) == false)
            {
                isIdele = true;
                animator?.Play("Idle_Battle");

                animator.StopRecording();
            }
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