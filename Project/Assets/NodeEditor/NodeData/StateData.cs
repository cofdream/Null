using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class StateData
    {
        public string StateName;
        public int InstanceId;
        public StateActionData[] StateAction;
        public TransitionData[] Transitions;
    }
}