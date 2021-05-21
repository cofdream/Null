

namespace DA.Core.FSM
{
    [UnityEngine.CreateAssetMenu(menuName = "State/Create State")]
    public class State : UnityEngine.ScriptableObject
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
            ExecuteActions(OnEnters);
        }
        public void OnUpdate()
        {
            ExecuteActions(OnUpdates);
        }
        public void OnFixedUpdate()
        {
            ExecuteActions(OnFixedUpdates);
        }
        public void OnExit()
        {
            ExecuteActions(OnExits);
        }

        private void ExecuteActions(StateAction[] stateActions)
        {
           
        }

        public Transition CheckTransitions()
        {
            if (Transitions == null) return null;

            foreach (var transition in Transitions)
            {
                if (transition.Active)
                {
                    if (transition.Condition.CheckStateChange())
                    {
                        return transition;
                    }
                }
            }
            return null;
        }
    }
}