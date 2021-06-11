using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class ScriptableObjectClone : ScriptableObject
    {
        protected virtual void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {

        }
        protected T GetCloneInstance<T>(Dictionary<int, CloneData> allDependencies, T instance) where T : ScriptableObjectClone
        {
            if (instance == null)
            {
                return null;
            }
            int instanceId = instance.GetInstanceID();
            if (instanceId == 0)
            {
                Debug.LogError($"clone target: {instance.name} instacne id == 0.");
                return null;
            }
            var cloneInstance = allDependencies[instanceId].CloneInstance as T;
            cloneInstance.CloneDependencies(allDependencies);
            return cloneInstance;
        }
    }

    public class CloneData
    {
        public ScriptableObjectClone Instance;
        public ScriptableObjectClone CloneInstance;
    }
}