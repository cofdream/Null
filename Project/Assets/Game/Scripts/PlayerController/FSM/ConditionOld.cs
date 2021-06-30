
namespace Game.FSM
{
    [System.Serializable]
    public class ConditionOld : DAScriptableObject
    {
        public string Description;
        public virtual bool CheckStateChange()
        {
            return true;
        }
    }
}