

namespace DA.Core.FSM
{
    [UnityEngine.CreateAssetMenu(menuName = "State/Create State")]
    public class State : UnityEngine.ScriptableObject
    {
        public StateAction[] OnEnters;
        public StateAction[] OnUpdates;
        public StateAction[] OnFixedUpdates;
        public StateAction[] OnExits;

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
            foreach (var item in stateActions)
            {
                if (item.Active) item.Execute();
            }
        }

        public Transition CheckTransitions()
        {
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