using System;
using System.Reflection;
using UnityEngine;

namespace DA.Singleton
{
    /// <summary>
    /// Singleton Util
    /// Use tip
    /// private static SingletonA instance = DA.Singleton.SingletonUtil.QuickCreateSingleton<SingletonA>();
    /// public static SingletonA Instance { get { return instance; } }
    /// [System.Obsolete("Not Use", true)]
    /// public SingletonA() { }
    /// </summary>
    public static class SingletonUtil
    {
        public static T CreateSingleton<T>() where T : class
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception($"{typeof(T)} need private constructor");
            }
            return ctor.Invoke(null) as T;
        }

        /// <summary>
        /// 快速创建单例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T QuickCreateSingleton<T>() where T : class, new()
        {
            var instance = new T();
            return instance;
        }
        public static T CreateSingletonMonoBehaviour<T>() where T : MonoBehaviour
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