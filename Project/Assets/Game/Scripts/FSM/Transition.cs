using UnityEngine;

namespace Game.FSM
{
    [System.Serializable]
    public class Transition
    {
        public int id;
        public bool Active = true;
        [SerializeReference] public Condition Condition;
        [HideInInspector] public State TargetState;
    }
}