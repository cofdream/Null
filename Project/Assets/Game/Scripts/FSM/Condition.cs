
namespace Game.FSM
{
    [System.Serializable]
    public class Condition : DAScriptableObject
    {
        public string Description;
        public virtual bool CheckStateChange()
        {
            return false;
        }
    }
}