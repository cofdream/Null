using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class StateDataGraph
    {
        public string StateName;
        public int InstanceId;
        public StateActionData[] StateAction;
        public TransitionData[] Transitions;
    }
}