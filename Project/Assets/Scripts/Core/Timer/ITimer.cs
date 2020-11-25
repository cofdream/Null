

namespace DA.Timer
{
    public delegate void TimeCallBack();
    public interface ITimer
    {
        bool Update(float time);
        void Dispose();
    }
}