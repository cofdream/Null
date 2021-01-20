using System;
using System.Collections.Generic;

namespace DA.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private Stack<T> objectStack = new Stack<T>();

        public event Func<T> CreateObjectAction;
        public event Action<T> DestoryObjectAction;

        public event Action<T> GetObjectAction;
        public event Action<T> ReleaseObjectAction;

        public bool BeyondPoolMaxCountImmediateDestory;

        private int poolMaxCount;

        public void Initialize(Func<T> createObjectAction = null, Action<T> destoryObjectAction = null, Action<T> getObjectAction = null, Action<T> releaseObjectAction = null,
                          int capacity = 10, int poolMaxCount = 10,
                          bool beyondPoolMaxCountImmediateDestory = true)
        {
            CreateObjectAction = createObjectAction;
            DestoryObjectAction = destoryObjectAction;
            GetObjectAction = getObjectAction;
            ReleaseObjectAction = releaseObjectAction;

            objectStack = new Stack<T>(capacity);
            this.poolMaxCount = poolMaxCount;

            BeyondPoolMaxCountImmediateDestory = beyondPoolMaxCountImmediateDestory;
        }
        public T Allocate()
        {
            T _object;
            if (objectStack.Count != 0)
                _object = objectStack.Pop();
            else
            {
                if (CreateObjectAction == null)
                    _object = default;
                else
                    _object = this.CreateObjectAction.Invoke();
            }

            GetObjectAction?.Invoke(_object);
            return _object;
        }
        public void Release(T _object)
        {
            ReleaseObjectAction?.Invoke(_object);

            if (objectStack.Count >= poolMaxCount)
            {
                if (BeyondPoolMaxCountImmediateDestory)
                {
                    DestoryObjectAction?.Invoke(_object);
                }
                else
                    objectStack.Push(_object);
            }
            else
                objectStack.Push(_object);
        }
        public void ClearPool()
        {
            if (DestoryObjectAction != null)
            {
                foreach (var _object in objectStack)
                {
                    DestoryObjectAction.Invoke(_object);
                }
            }

            objectStack.Clear();
        }
    }
}