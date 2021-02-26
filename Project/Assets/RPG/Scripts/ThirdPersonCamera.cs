using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        public float speed;

        Vector3 offest;

        PlayerInput inputs;

        private void Awake()
        {
            transform.position = followTarget.position + new Vector3(0f, 3.5f, -6.5f);
            transform.rotation = Quaternion.Euler(followTarget.eulerAngles + new Vector3(10f, 0f, 0f));

            offest = transform.position - followTarget.position;

            inputs = new PlayerInput();
        }
        private void OnEnable()
        {
            inputs.Enable();
        }
        private void OnDisable()
        {
            inputs.Disable();
        }
        float mousPositionX;
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

                float offset = mouseDelta.x * 360 / Screen.width;
                mousPositionX += offset;

                transform.RotateAround(followTarget.position, followTarget.up, offset);

                offest = transform.position - followTarget.position;
            }
            else
            {
                // 还原之前的旋转量
                transform.RotateAround(followTarget.position, followTarget.up, mousPositionX * -1);
                mousPositionX = 0;

                offest = transform.position - followTarget.position;
            }
        }
    }
}