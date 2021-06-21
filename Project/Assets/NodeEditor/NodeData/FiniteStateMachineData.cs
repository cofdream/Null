using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class FiniteStateMachineData : ScriptableObject
    {
        public string Name;
        public StateData[] AllStates;
    }
}