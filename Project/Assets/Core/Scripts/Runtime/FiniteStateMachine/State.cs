using System;

namespace Core
{
    [Serializable]
    public class State<T> where T : FiniteStateMachineBase<T>
    {
        public StateAction<T>[] EnterActions;
        public StateAction<T>[] UpdateActions;
        public StateAction<T>[] ExitActions;

        public Condition<T>[] ConditionActions;

        public StateAction<T>[] FixUpdateActions;

        public virtual void Enter(T fsm)
        {
            if (EnterActions != null)
            {
                foreach (var action in EnterActions)
                {
                    action.Execute(fsm);
                }
            }
        }

        public virtual void Exit(T fsm)
        {
            if (ExitActions != null)
            {
                foreach (var action in ExitActions)
                {
                    action.Execute(fsm);
                }
            }
        }

        public virtual void Update(T fsm)
        {
            if (UpdateActions != null)
            {
                foreach (var action in UpdateActions)
                {
                    action.Execute(fsm);
                }
            }

            if (ConditionActions != null)
            {
                foreach (var condition in ConditionActions)
                {
                    if (condition.Update(fsm))
                    {
                        fsm.CurrentState.Exit(fsm);

                        fsm.LastState = fsm.CurrentState;

                        fsm.CurrentState = condition.TargetState;
                        fsm.CurrentState.Enter(fsm);
                    }
                }
            }
        }

        public virtual void FixUpdate(T fsm)
        {
            if (FixUpdateActions != null)
            {
                foreach (var action in FixUpdateActions)
                {
                    action.Execute(fsm);
                }
            }
        }

        public virtual void BackLastState(T fsm)
        {
            fsm.CurrentState.Exit(fsm);
            fsm.CurrentState = lastState;
            lastState.Enter(fsm);
        }
    }
}