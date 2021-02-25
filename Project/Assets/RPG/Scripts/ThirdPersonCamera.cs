using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;

        Vector3 offest;

        PlayerInput inputs;

        private void Awake()
        {
            transform.position = followTarget.position + new Vector3(0f, 3.5f, -6.5f);
            transform.rotation = Quaternion.Euler(followTarget.eulerAngles + new Vector3(10f, 0f, 0f));

            offest = transform.position - followTarget.position;

            inputs = new PlayerInput();

            //inputs.Player.LeftMouse.performed += (callback) =>
            //{
            //    //Debug.Log("performed");
            //};
            //inputs.Player.LeftMouse.started += (callback) =>
            //{
            //    //Debug.Log("started");
            //};
            //inputs.Player.LeftMouse.canceled += (callback) =>
            //{
            //    //Debug.Log("canceled");
            //};
        }
        private void OnEnable()
        {
            inputs.Enable();
        }
        private void OnDisable()
        {
            inputs.Disable();
        }

        private void LateUpdate()
        {
            var v = inputs.Player.RightMouse.ReadValue<float>();
            var v2 = inputs.Player.LeftMouse.ReadValue<float>();

            transform.position = followTarget.position + offest;

            if (v == 1 || v2 == 1)
            {
                Vector2 mouseDelta = inputs.Player.MouseDelta.ReadValue<Vector2>();
                if (mouseDelta.x == 0)
                {
                    return;
                }
                float angle;
                if (mouseDelta.x > 0)
                {
                    angle = 1;
                }
                else
                {
                    angle = -1;
                }

                transform.RotateAround(followTarget.position, followTarget.up, angle * 8);

                offest = transform.position - followTarget.position;
            }
        }
    }
}