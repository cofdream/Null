
namespace DA.DataModule
{
    public delegate void BindAction<T>(T old, T @new);

    public interface IBind<T>
    {
        void Bind(BindAction<T> callBack);
        void Unbind(BindAction<T> callBack);
    }

    public class DataBind<T> : IBind<T>
    {
        private bool firste = true;
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                T temp = this.value;
                if (firste)
                {
                    firste = false;

                    this.value = value;
                    this.callback.Invoke(this.value, value);
                    return;
                }
                if (temp.Equals(value) == false)
                {
                    this.value = value;
                    this.callback.Invoke(temp, value);
                }
            }
        }

        private BindAction<T> callback;

        public DataBind(BindAction<T> callBack)
        {
            this.callback = callBack;
        }

        public void Bind(BindAction<T> callBack)
        {
            this.callback += callback;
        }

        public void Unbind(BindAction<T> callBack)
        {
            this.callback -= callback;
        }
    }
}