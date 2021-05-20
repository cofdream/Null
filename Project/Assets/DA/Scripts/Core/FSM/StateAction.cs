

namespace DA.Core.FSM
{

    public class StateAction : UnityEngine.ScriptableObject
    {
        public bool Active;
        public State state;
        public virtual void Execute()
        {

        }
    }
}