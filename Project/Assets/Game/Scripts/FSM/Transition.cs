using System;
using UnityEngine;

namespace Game.FSM
{
    [Serializable]
    public class Transition : DAScriptableObject
    {
        public int Id;
        public bool Active = true;
        [SerializeReference] public Condition Condition;
        [SerializeReference] public State TargetState;
    }
}