using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA.Event
{
    public interface IDispatcher
    {
        void Subscribe<T>(short type, EventHandler<T> handler);
        void Unsubscribe<T>(short type, EventHandler<T> handler);
        void Subscribe(short type, EventHandler handler);
        void Unsubscribe(short type, EventHandler handler);
    }
}