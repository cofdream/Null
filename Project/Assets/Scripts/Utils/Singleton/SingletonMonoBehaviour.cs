using System;
using UnityEngine;

namespace Cofdream.Utils
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingleton where T : SingletonMonoBehaviour<T>
    {
        static SingletonMonoBehaviour() { }

        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance != null)
                    {
                        var instances = FindObjectsOfType<T>();

                        if (instances.Length == 1) throw new Exception(typeof(T) + " Instance Existed!");

                        throw new Exception(typeof(T) + " Instance Existed! And Type Count : " + instances.Length.ToString());
                    }
                    var gameObject = new UnityEngine.GameObject(typeof(T).Name + "Singleton No Copy");

                    DontDestroyOnLoad(gameObject);

                    instance = gameObject.AddComponent<T>();

                    instance.SingletonInit();
                }
                return instance;
            }
        }

        protected virtual void SingletonInit() { }
        public void Free()
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
            instance = null;
        }
    }
}