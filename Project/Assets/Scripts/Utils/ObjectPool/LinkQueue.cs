using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA.Utils
{
    public class LinkQueue<T> : IEnumerator<T>
    {
        private int count;
        public int Count { get { return count; } }

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        internal void Enqueue(T t)
        {
            throw new NotImplementedException();
        }

        internal T Dequeue()
        {
            throw new NotImplementedException();
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}