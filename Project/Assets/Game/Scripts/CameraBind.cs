using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraBind : MonoBehaviour
    {
        public CinemachineVirtualCamera CM_VCamera;
        private GameObject newGo;

        private void Awake()
        {
            newGo = new GameObject();
            newGo.transform.parent = transform;
        }

        private float x;
        private float y;
        public void LateUpdate()
        {
             x += Input.GetAxis("Mouse X");
             y += Input.GetAxis("Mouse Y");

            x = Mathf.Clamp(x, -35f, 35f);
            y = Mathf.Clamp(y, -35f, 35f);

            CM_VCamera.transform.localRotation = Quaternion.Euler(-y, x, 0);

            newGo.transform.localRotation = Quaternion.Euler(-y, x, 0);

            Debug.Log(x + "," + -y);
        }
    }
}