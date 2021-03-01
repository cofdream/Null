
namespace DA.Singleton
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly T instance;
        public static T Instance { get { return instance; } }
        static Singleton()
        {
            instance = SingletonUtil.CreateSingleton<T>();
            instance.InitSingleton();
        }
        protected virtual void InitSingleton() { }
    }
}