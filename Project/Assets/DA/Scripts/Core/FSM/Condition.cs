

namespace DA.Core.FSM
{
    [System.Serializable]
    public class Condition : UnityEngine.ScriptableObject
    {
        public string Description;
        public virtual bool CheckStateChange()
        {
            return false;
        }
    }
}