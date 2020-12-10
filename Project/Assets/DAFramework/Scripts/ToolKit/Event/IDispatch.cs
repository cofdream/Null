using UnityEngine;

namespace DA.Event
{
    public interface IDispatch
    {
        IDispatcher Dispatcher { get; }
    }
}