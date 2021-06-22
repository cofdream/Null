using System;
using System.Reflection;
using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class NodeDataGraph : ScriptableObject
    {
        public FiniteStateMachineData FiniteStateMachineData;

        public Rect NodeRect;

        public Type Type;
        public FieldInfo[] FieldInfos;
        public bool[] Toogles;

        private void OnValidate()
        {
            Type = FiniteStateMachineData.GetType();
            FieldInfos = Type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            Toogles = new bool[FieldInfos.Length];
        }
    }


}