
namespace DA.Core.FSM
{
    public class FSM 
    {
        public State CurrentState;

        public void OnUpdate()
        {
            CurrentState.OnUpdate();
            var targetState = CurrentState.CheckTransitions();
            if (targetState != null)
            {
                ChangeState(targetState);
            }
        }

        public void OnFixedUpdate()
        {
            CurrentState.OnFixedUpdate();
        }

        public void ChangeState(State targetState)
        {
            CurrentState.OnExit();
            targetState.OnEnter();
            CurrentState = targetState;
        }
    }
}