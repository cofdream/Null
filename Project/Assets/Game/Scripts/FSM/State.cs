using System;
using UnityEngine;

namespace Game.FSM
{
    [Serializable]
    public class State
    {
        public string StateName;
        [SerializeReference] public StateAction[] StateAction;
        [SerializeReference] public Transition[] Transitions;

        public void OnEnter()
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
        public void OnUpdate()
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
        public void OnFixedUpdate()
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
        public void OnExit()
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

        public State CheckTransitions()
        {
            if (Transitions == null) return null;

            foreach (var transition in Transitions)
            {
                if (transition.Active)
                {
                    if (transition.Condition.CheckStateChange())
                    {
                        return transition.TargetState;
                    }
                }
            }
            return null;
        }
    }
}