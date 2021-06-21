using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class FiniteStateMachineDataGraph : ScriptableObject
    {
        public FiniteStateMachineData FiniteStateMachineData;
        public string Name;
        public StateData[] AllStates;

        public void OnValidate()
        {
            Name = FiniteStateMachineData.Name;
            AllStates = FiniteStateMachineData.AllStates;
        }


        public Rect NodeRect;
    }
}