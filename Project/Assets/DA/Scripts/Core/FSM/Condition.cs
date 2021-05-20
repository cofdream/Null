

namespace DA.Core.FSM
{
    [System.Serializable]
    public class Condition : UnityEngine.ScriptableObject
    {
        public string Description { get; }
        public bool CheckStateChange()
        {
            return false;
        }
    }
}