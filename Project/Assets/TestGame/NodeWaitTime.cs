[System.Serializable]
public class NodeWaitTime : SKillNode
{
    public float waitTime;
    public override void Run()
    {
        DA.Timer.Timer.StartTimer(new DA.Timer.TimerDisposable(waitTime, nextStep));
    }
}