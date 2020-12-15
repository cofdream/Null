

namespace DA.Timer
{
    public interface ITimer
    {
        bool Update(float time);
        void Dispose();
    }
}