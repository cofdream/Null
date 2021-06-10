using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Game
{
    public class DAScriptableObject : ScriptableObject
    {
        public int InstanceID;

        private void OnValidate()
        {
            InstanceID = GetInstanceID();
        }

        public virtual void CloneVariables(Dictionary<int, CloneData> allDependencies)
        {

        }
        protected T GetCloneInstance<T>(Dictionary<int, CloneData> allDependencies, T instance) where T : DAScriptableObject
        {
            return allDependencies[instance.InstanceID].CloneInstance as T;
        }
    }

    public class CloneData
    {
        public DAScriptableObject Instance;
        public DAScriptableObject CloneInstance;
    }
}