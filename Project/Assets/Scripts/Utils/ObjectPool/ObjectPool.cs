using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA.Utils
{

    public class ObjectPool<T> : IObjectPool<T> where T : class, IPoolObject
    {
        public int Count;
        readonly List<T> objs = new List<T>();

        public ObjectPool(int count = 10)
        {
            Count = count;
            objs = new List<T>(Count);
        }
        public void Set(T t)
        {
            if (objs.Count == Count)
            {
                t.Free();
            }
            else
            {
                objs.Add(t);
            }
        }

        public T Get()
        {
            int index = objs.Count - 1;
            if (index == -1) return null;
            T item = objs[index];
            objs.RemoveAt(index);

            return item;
        }
        public void Free()
        {
            foreach (var item in objs)
            {
                item.Free();
            }
            objs.Clear();
        }
    }
    public class ObjectPoolTime<T> : IObjectPoolTime<T> where T : class, IPoolObjectTime
    {
        public int Count;
        readonly List<T> objs;

        public ObjectPoolTime(int count = 10)
        {
            Count = count;
            objs = new List<T>(Count);
        }

        public void Set(T t)
        {
            if (objs.Count == Count)
            {
                t.Free();
            }
            else
            {
                t.LeftCacheTime = 0;
                objs.Add(t);
            }
        }

        public T Get()
        {
            int index = objs.Count - 1;
            if (index == -1) return null;
            T item = objs[index];
            objs.RemoveAt(index);

            return item;
        }
        public void Free()
        {
            foreach (var item in objs)
            {
                item.Free();
            }
            objs.Clear();
        }

        void IObjectPoolTime<T>.Update(float time)
        {
            int count = objs.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                var item = objs[i];

                item.LeftCacheTime += time;
                if (item.LeftCacheTime > item.CacheTime)
                {
                    item.Free();
                    objs[i] = objs[count];
                    objs.RemoveAt(count);
                    count--;
                }
            }
        }
    }


    public interface IObjectPool<T> where T : class, IPoolObject
    {
        T Get();
        void Set(T t);
        void Free();
    }

    public interface IPoolObject
    {
        void Free();
    }


    public interface IObjectPoolTime<T> : IObjectPool<T> where T : class, IPoolObjectTime
    {
        void Update(float time);
    }

    public interface IPoolObjectTime : IPoolObject
    {
        float LeftCacheTime { get; set; }
        int CacheTime { get; set; }
    }
}