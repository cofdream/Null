

namespace DA.Core.FSM
{
    [System.Serializable]
    public class Transition : UnityEngine.ScriptableObject
    {
        public int id;
        public bool Active;
        public Condition Condition;
        public State TargetState;
    }
}