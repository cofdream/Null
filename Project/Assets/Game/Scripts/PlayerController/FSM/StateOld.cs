using System;
using UnityEngine;

namespace Game.FSM
{
    [Serializable]
    public class StateOld : DAScriptableObject
    {
        public string StateName;
        [SerializeReference] public StateActionOld[] StateAction;
        [SerializeReference] public TransitionOld[] Transitions;

        public virtual void OnEnter()
        {
            if (StateAction == null) return;
            foreach (var stateAction in StateAction)
            {
                if (stateAction.Active)
                {
                    stateAction.OnEnter();
                }
            }
        }
        public virtual void OnUpdate()
        {
            if (StateAction == null) return;
            foreach (var stateAction in StateAction)
            {
                if (stateAction.Active)
                {
                    stateAction.OnUpdate();
                }
            }
        }
        public virtual void OnFixedUpdate()
        {
            if (StateAction == null) return;
            foreach (var stateAction in StateAction)
            {
                if (stateAction.Active)
                {
                    stateAction.OnFixedUpdate();
                }
            }
        }
        public virtual void OnExit()
        {
            if (StateAction == null) return;
            foreach (var stateAction in StateAction)
            {
                if (stateAction.Active)
                {
                    stateAction.OnExit();
                }
            }
        }

        public virtual bool CheckTransitions(out StateOld targetState)
        {
            targetState = null;
            if (Transitions == null) return false;

            foreach (var transition in Transitions)
            {
                if (transition.Active)
                {
                    if (transition.Condition.CheckStateChange())
                    {
                        targetState = transition.TargetState;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}