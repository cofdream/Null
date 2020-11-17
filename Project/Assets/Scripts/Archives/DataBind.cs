using System;

namespace DA.DataModule
{
    public sealed class DataBind<T>
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
                    this.cahngeValue?.Invoke(this.value, value);
                    return;
                }
                if (temp.Equals(value) == false)
                {
                    this.value = value;
                    this.cahngeValue?.Invoke(temp, value);
                }
            }
        }

        private Action<T, T> cahngeValue;

        public DataBind(T value)
        {
            this.value = value;
        }

        public void Bind(Action<T, T> cahngeValue)
        {
            this.cahngeValue += cahngeValue;
        }

        public void Unbind(Action<T, T> cahngeValue)
        {
            this.cahngeValue -= cahngeValue;
        }
    }
}