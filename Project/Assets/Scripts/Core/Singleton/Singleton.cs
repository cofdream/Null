using System;
using System.Reflection;
using UnityEngine;

namespace DA
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly T instance;

        static Singleton()
        {
            instance = Singleton.GetSingleton<T>();
            instance.InitSingleton();
        }

        public static T Instance { get { return instance; } }

        protected virtual void InitSingleton() { }
    }

    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        static SingletonMonoBehaviour() { instance = Singleton.GetSingletonMono<T>(); }

        private static readonly T instance;

        public static T Instance { get { return instance; } }
    }

    public static class Singleton
    {
        public static T GetSingleton<T>() where T : class
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception($"{typeof(T)} need private constructor");
            }
            return ctor.Invoke(null) as T;
        }
        public static T GetSingleton_Quick<T>() where T : class, new()
        {
            var instance = new T();
            return instance;
        }

        public static T GetSingletonMono<T>() where T : MonoBehaviour
        {
            T instance = UnityEngine.Object.FindObjectOfType<T>();

            if (instance != null)
            {
                var instances = UnityEngine.Object.FindObjectsOfType<T>();

                if (instances.Length == 1) throw new Exception(typeof(T) + " Instance Existed!");

                throw new Exception(typeof(T) + " Instance Existed! And Type Count : " + instances.Length.ToString());
            }
            var gameObject = new UnityEngine.GameObject("Singleton No Copy");

            UnityEngine.Object.DontDestroyOnLoad(gameObject);

            instance = gameObject.AddComponent<T>();

            return instance;
        }
    }
}