using System;
using System.Collections.Generic;

namespace DA.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        public event Action<T> GetObjectAction;
        public event Action<T> ReleaseObjectAction;

        private readonly Stack<T> objectStack = new Stack<T>();
        private readonly Func<T> createObjectAction;
        private readonly Action<T> destoryObjectAction;
        private int poolMaxCount;

        public ObjectPool(Func<T> createObjectAction = null, Action<T> destoryObjectAction = null, int capacity = 10, int poolMaxCount = 10)
        {
            this.poolMaxCount = poolMaxCount;

            objectStack = new Stack<T>(capacity);

            this.createObjectAction = createObjectAction;
            this.destoryObjectAction = destoryObjectAction;
        }
        public T Allocate()
        {
            T _object;
            if (objectStack.Count == 0)
                _object = this.createObjectAction.Invoke();
            else
                _object = objectStack.Pop();

            GetObjectAction?.Invoke(_object);
            return _object;
        }
        public void Release(T _object)
        {
            ReleaseObjectAction?.Invoke(_object);

            if (objectStack.Count < poolMaxCount)
            {
                objectStack.Push(_object);
            }
        }
        public void ClearPool()
        {
            foreach (var _object in objectStack)
            {
                destoryObjectAction?.Invoke(_object);
            }
            objectStack.Clear();
        }
    }

    public class ObjectPoolTimer<T> : IObjectPool<T> where T : class
    {
        public event Action<T> GetObjectAction;
        public event Action<T> ReleaseObjectAction;

        public int Count;
        readonly List<T> objs;

        private readonly Stack<T> objectStack = new Stack<T>();
        private readonly Func<T> createObjectAction;
        private readonly Action<T> destoryObjectAction;
        private int poolMaxCount;

        public ObjectPoolTimer(Func<T> createObjectAction = null, Action<T> destoryObjectAction = null, int capacity = 10, int poolMaxCount = 10)
        {
            this.poolMaxCount = poolMaxCount;

            objectStack = new Stack<T>(capacity);

            this.createObjectAction = createObjectAction;
            this.destoryObjectAction = destoryObjectAction;


            Timer.Timer.AddTimer(new Timer.TimerMulti()
            {
                CallBack = Update,
            });
        }
        public T Allocate()
        {
            T _object;
            if (objectStack.Count == 0)
                _object = this.createObjectAction.Invoke();
            else
                _object = objectStack.Pop();

            GetObjectAction?.Invoke(_object);
            return _object;
        }
        public void Release(T _object)
        {
            ReleaseObjectAction?.Invoke(_object);

            if (objectStack.Count < poolMaxCount)
            {
                objectStack.Push(_object);
            }
        }

        public void ClearPool()
        {
            foreach (var _object in objectStack)
            {
                destoryObjectAction?.Invoke(_object);
            }
            objectStack.Clear();
        }

        private void Update()
        {

        }
    }


    public interface IObjectPool<T> where T : class
    {
        event Action<T> GetObjectAction;
        event Action<T> ReleaseObjectAction;

        T Allocate();
        void Release(T _object);
        void ClearPool();
    }
}