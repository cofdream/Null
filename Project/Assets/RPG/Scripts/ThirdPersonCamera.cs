using UnityEngine;

namespace RPG
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;

        public float speed;
        Vector3 offest;

        public float moveDistance = 3;
        private float startDistance;

        float deltaX;
        float deltaY;
        Vector3 startPosition;
        private bool isDown;

        private void Awake()
        {
            transform.position = followTarget.position + new Vector3(0f, 3.5f, -6.5f);
            transform.rotation = Quaternion.Euler(followTarget.eulerAngles + new Vector3(10f, 0f, 0f));

            offest = transform.position - followTarget.position;
            startDistance = Vector3.Distance(transform.position, followTarget.position);
        }

        private void LateUpdate()
        {
            RotateCamera();

            MouveCamera();
        }
        private void RotateCamera()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isDown = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isDown = true;
                deltaX = 0;
                deltaY = 0;
                startPosition = Input.mousePosition;
            }
            if (isDown == false)
            {
                return;
            }

            var mousePosition = Input.mousePosition;
            deltaX = mousePosition.x - startPosition.x;
            deltaY = mousePosition.y - startPosition.y;

            startPosition = mousePosition;

            transform.Rotate(Vector3.up, deltaX, Space.World);
            transform.Rotate(Vector3.left, deltaY, Space.Self);
        }

        private void MouveCamera()
        {
            var position = followTarget.position + offest;

            float distance = Vector3.Distance(transform.position, position);

            if (distance > moveDistance)
            {
                transform.position = Vector3.Lerp(transform.position, position, distance * Time.deltaTime);
            }
            else
            {
                if (distance < 0.01)
                {
                    //transform.position = followTarget.position + offest;
                }
                else
                {
                }
                    transform.position = Vector3.Lerp(transform.position, position, speed * 0.5f * Time.deltaTime);
            }
        }
    }
}