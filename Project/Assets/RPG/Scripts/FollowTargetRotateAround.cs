using UnityEngine;

namespace RPG
{
    public class FollowTargetRotateAround : MonoBehaviour
    {
        [SerializeField] Transform followTarget;
        public float rotateSpeed = 8;

        private void LateUpdate()
        {
            float v = Input.GetAxis("Mouse X") * rotateSpeed;
            float h = Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.RotateAround(followTarget.position, followTarget.up, v);
        }
    }
}