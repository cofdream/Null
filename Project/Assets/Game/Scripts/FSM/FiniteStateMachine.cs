
namespace Game.FSM
{
    [System.Serializable]
    public class FiniteStateMachine : ScriptableObjectClone
    {
        public State[] AllStates;

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