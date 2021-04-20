using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class FiniteStateMachinePlayer : FiniteStateMachineBase<FiniteStateMachinePlayer>
    {
        public float deltaTime;
        public Rigidbody Rigidbody;
        public Transform Transform;
        public Transform CameraTransform;
    }
}