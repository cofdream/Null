using UnityEngine;

namespace DA.Singleton
{
    /// <summary>
    /// 创建mono单例，不推荐
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        static SingletonMonoBehaviour() { instance = SingletonUtil.CreateSingletonMonoBehaviour<T>(); }

        private static readonly T instance;

        public static T Instance { get { return instance; } }
    }
}