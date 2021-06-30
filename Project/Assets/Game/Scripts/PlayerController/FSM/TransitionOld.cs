using System;
using UnityEngine;

namespace Game.FSM
{
    [Serializable]
    public class TransitionOld : DAScriptableObject
    {
        public int Id;
        public bool Active = true;
        [SerializeReference] public ConditionOld Condition;
        [SerializeReference] public StateOld TargetState;
    }
}