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
            if (instance == null)
            {
                return null;
            }
            if (instance.InstanceID == 0)
            {
                Debug.LogError($"clone target: {instance.name} instacne id == 0.");
            }
            return allDependencies[instance.InstanceID].CloneInstance as T;
        }
    }

    public class CloneData
    {
        public DAScriptableObject Instance;
        public DAScriptableObject CloneInstance;
    }
}