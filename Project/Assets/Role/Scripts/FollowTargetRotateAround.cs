using UnityEngine;

namespace RPG
{
    public class FollowTargetRotateAround : MonoBehaviour
    {
        [SerializeField] Transform followTarget;
        [SerializeField] float rotateSpeed = 8;

        float eulerX;
        float eulerY;

        private void LateUpdate()
        {
            eulerY += Input.GetAxis("Mouse X") * rotateSpeed;
            eulerX -= Input.GetAxis("Mouse Y") * rotateSpeed;

            eulerX = Mathf.Clamp(eulerX, -35f, 35f);

            transform.localRotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }
}