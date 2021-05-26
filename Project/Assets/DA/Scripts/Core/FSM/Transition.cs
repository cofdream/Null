

namespace DA.Core.FSM
{
    [System.Serializable]
    public class Transition 
    {
        public int id;
        public bool Active;
        public Condition Condition;
        public State TargetState;
    }
}