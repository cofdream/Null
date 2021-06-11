

namespace Game.FSM
{
    [System.Serializable]
    public class Condition : ScriptableObjectClone
    {
        public string Description;
        public virtual bool CheckStateChange()
        {
            return false;
        }
    }
}