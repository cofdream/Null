using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class FiniteStateMachinePlayer : FiniteStateMachineBase<FiniteStateMachinePlayer>
    {
        public Rigidbody Rigidbody;
    }
}