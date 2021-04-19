using System;

namespace Core
{
    [Serializable]
    public class State<T> where T : FiniteStateMachineBase<T>
    {
        public StateAction<T>[] EnterAction;
        public StateAction<T>[] UpdateAction;
        public StateAction<T>[] ExitAction;

        public Condition<T>[] conditions;

        public virtual void Enter(T fsm)
        {
            if (EnterAction != null)
            {
                foreach (var action in EnterAction)
                {
                    action.Execute(fsm);
                }
            }
        }

        public virtual void Exit(T fsm)
        {
            if (ExitAction != null)
            {
                foreach (var action in ExitAction)
                {
                    action.Execute(fsm);
                }
            }
        }

        public virtual void Update(T fsm)
        {
            if (UpdateAction != null)
            {
                foreach (var action in UpdateAction)
                {
                    action.Execute(fsm);
                }
            }

            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    if (condition.Update(fsm))
                    {
                        fsm.CurrentState.Exit(fsm);
                        fsm.CurrentState = condition.TargetState;
                        condition.TargetState.Enter(fsm);
                    }
                }
            }
        }
    }
}