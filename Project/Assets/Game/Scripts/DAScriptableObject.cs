using UnityEngine;

namespace Game
{
    public class DAScriptableObject : ScriptableObject
    {
        protected int InstanceId;

        private void OnEnable()
        {
            InstanceId = GetInstanceID();
        }
        protected void DeepCopy()
        {

        }
    }
    public interface IDADeepCopy<T> where T : DAScriptableObject
    {
        T GetDeepCopyObject(int instanceId, bool isCopy);
    }
}