using UnityEngine;

namespace DA.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static readonly T instance;
        public static T Instance { get { return instance; } }
        static SingletonMonoBehaviour() { instance = SingletonUtil.CreateSingletonMonoBehaviour<T>(); }
    }
}