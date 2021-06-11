

namespace Game.FSM
{
    [System.Serializable]
    public class Transition : ScriptableObjectClone
    {
        public int id;
        public bool Active = true;
        public Condition Condition;
        public State TargetState;
    }
}