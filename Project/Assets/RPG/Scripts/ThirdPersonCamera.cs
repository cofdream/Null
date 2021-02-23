using UnityEngine;

namespace RPG
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Transform Target
        {
            get { return target; }
            set
            {
                target = value;
                Start();
            }
        }
        private void Start()
        {
            transform.position = target.position + new Vector3(0f, 3.4f, -5.2f);
            transform.rotation = Quaternion.Euler(target.eulerAngles + new Vector3(10f, 0f, 0f));
        }
        private void LateUpdate()
        {
            
        }
    }
}