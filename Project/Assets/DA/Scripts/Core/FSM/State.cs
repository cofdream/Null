

namespace DA.Core.FSM
{
    [System.Serializable]
    public class State 
    {
        public StateAction[] StateAction;

        public Transition[] Transitions;

        public Transition GetTransitions(int index)
        {
            foreach (var transition in Transitions)
            {
                if (transition.id == index)
                {
                    return transition;
                }
            }
            return null;
        }

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