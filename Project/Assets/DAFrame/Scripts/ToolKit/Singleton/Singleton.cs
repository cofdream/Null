
namespace DA.Singleton
{
    /// <summary>
    /// 创建单例 ，不推荐
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly T instance;

        static Singleton()
        {
            instance = SingletonUtil.CreateSingleton<T>();
            instance.InitSingleton();
        }

        public static T Instance { get { return instance; } }

        protected virtual void InitSingleton() { }
    }
}