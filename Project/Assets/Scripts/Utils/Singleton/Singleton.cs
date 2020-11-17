using System;
using System.Reflection;

namespace DA.Utils
{
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        static Singleton() { @lock = new object(); }

        protected static T instance;

        protected static object @lock;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (@lock)
                    {

                        var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                        var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                        if (ctor == null)
                        {
                            throw new Exception("Non Constructor() not found! in " + typeof(T));
                        }

                        instance = ctor.Invoke(null) as T;
                        instance.InitSingleton();

                        @lock = null;
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// 单例初始化
        /// </summary>
        protected virtual void InitSingleton() { }
        /// <summary>
        /// 释放单例，如有需要
        /// </summary>
        public virtual void Free() { }
        /// <summary>
        /// 提前加载单例
        /// </summary>
        public virtual void LoadSingleton() { }
    }
}
