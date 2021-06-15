using UnityEngine;

namespace Game.FSM
{
    [System.Serializable]
    public class FiniteStateMachine
    {
        [SerializeReference] public State[] AllStates;

        [SerializeReference] public State CurrentState;

        public void Update()
        {
            CurrentState.OnUpdate();
            var targetState = CurrentState.CheckTransitions();
            if (targetState != null)
            {
                ChangeState(targetState);
            }
        }

        public void FixedUpdate()
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