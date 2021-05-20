

namespace DA.Core.FSM
{
    public class FSM : UnityEngine.ScriptableObject
    {
        public State CurrentState;

        public void OnUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.OnUpdate();
                var transition = CurrentState.CheckTransitions();
                if (transition != null)
                {
                    ChangeState(transition.TargetState);
                }
            }
        }

        public void OnFixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.OnFixedUpdate();
            }
        }

        public void ChangeState(State targetState)
        {
            CurrentState.OnExit();
            targetState.OnEnter();
            CurrentState = targetState;
        }
    }
}