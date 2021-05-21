
namespace DA.Core.FSM
{
    public class FSM : UnityEngine.ScriptableObject
    {
        public State CurrentState;

        public void OnUpdate()
        {
            CurrentState.OnUpdate();
            var transition = CurrentState.CheckTransitions();
            if (transition != null)
            {
                ChangeState(transition.TargetState);
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