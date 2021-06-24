using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class StateData
    {
        public string StateName = "State";
        public StateActionData[] StateAction;
        public TransitionData[] Transitions;
    }
}