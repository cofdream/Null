using System;
using System.Reflection;
using UnityEngine;

namespace NullNamespace
{
    [System.Serializable]
    public class NodeDataGraph : ScriptableObject
    {
        public FiniteStateMachineData FiniteStateMachineData;
        public bool Toogle;


        public Rect NodeRect;

        public Type DataType;

        public FieldInfo[] FieldInfos;
        public bool[] Toogles;

        private void OnValidate()
        {
            DataType = FiniteStateMachineData.GetType();
            FieldInfos = DataType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            Toogles = new bool[FieldInfos.Length];

            if (FiniteStateMachineData != null)
            {
                if (true)
                {

                }
            }
        }
    }


}