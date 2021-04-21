
namespace Core
{
    [System.Serializable]
    public abstract class StateAction<T> where T : FiniteStateMachine<T>
    {
        public abstract void Execute(T fsm);
    }
}