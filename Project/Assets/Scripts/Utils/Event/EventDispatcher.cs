using System;
using System.Collections.Generic;

namespace Cofdream.Event
{
    public delegate void EventHandler(short type);
    public delegate void EventHandler<T>(short type, T msg);
    public class EventDispatcher
    {
        private Dictionary<short, List<Delegate>> events = null;
        private List<List<Delegate>> removeHandles = null;
        public EventDispatcher()
        {
            events = new Dictionary<short, List<Delegate>>(10);
            removeHandles = new List<List<Delegate>>();
        }

        public void ClearAllHandle()
        {
            var allHandle = events.GetEnumerator();
            foreach (var delegates in events.Values)
            {
                delegates.Clear();
            }
            removeHandles.Clear();
        }
        public void ClearNullHandle()
        {
            int length = removeHandles.Count;
            for (int i = 0; i < length; i++)
            {
                var delegates = removeHandles[i];
                int oldLength = delegates.Count;
                int newLength = 0;
                for (int j = 0; j < oldLength; j++)
                {
                    if (delegates[j] != null)
                    {
                        if (newLength != j)
                        {
                            delegates[newLength] = delegates[j];
                        }
                        newLength++;
                    }
                }
                delegates.RemoveRange(newLength, oldLength - newLength);
            }
        }
        public void Subscribe<T>(short type, EventHandler<T> handler)
        {
            Delegate @delegate = handler;
            AddHandle(events, type, @delegate);
        }
        public void Unsubscribe<T>(short type, EventHandler<T> handler)
        {
            Delegate @delegate = handler;
            RemoveHandle(events, type, @delegate);
        }
        public void SendEvent<T>(short type, T msg)
        {
            Send(events, type, msg);
        }

        private void AddHandle(Dictionary<short, List<Delegate>> dic, short type, Delegate handle)
        {
            List<Delegate> delegates = null;
            if (dic.TryGetValue(type, out delegates))
            {
                delegates.Add(handle);
            }
            else
            {
                dic.Add(type, new List<Delegate>() { handle });
            }
        }
        private void RemoveHandle(Dictionary<short, List<Delegate>> dic, short type, Delegate handle)
        {
            List<Delegate> delegates = null;
            if (dic.TryGetValue(type, out delegates))
            {
                if (delegates != null)
                {
                    int index = delegates.IndexOf(handle);
                    if (index != -1)
                    {
                        delegates[index] = null;
                    }

                    if (removeHandles.Contains(delegates) == false)
                    {
                        removeHandles.Add(delegates);
                    }
                }
            }
        }

        private void Send<T>(Dictionary<short, List<Delegate>> dic, short type, T msg)
        {
            List<Delegate> delegates = null;
            if (dic.TryGetValue(type, out delegates))
            {
                Send(delegates, type, msg);
            }
        }
        private void Send<T>(List<Delegate> delegates, short type, T msg)
        {
            int length = delegates.Count;
            for (int i = 0; i < length; i++)
            {
                var handler = delegates[i] as EventHandler<T>;
                if (handler != null)
                {
                    handler.Invoke(type, msg);
                }
            }
        }
    }
}