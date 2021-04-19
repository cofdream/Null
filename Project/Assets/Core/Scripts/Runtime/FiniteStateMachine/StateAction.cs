
namespace Core
{
    [System.Serializable]
    public abstract class StateAction<T>  where T : FiniteStateMachineBase<T>
    {
        public abstract void Execute(T fsm);
    }
}