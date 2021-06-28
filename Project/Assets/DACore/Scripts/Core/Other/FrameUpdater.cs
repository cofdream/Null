using System;

namespace DA
{
    public partial class FrameUpdater
    {
        private static float deltaTime;
        public static float DeltaTime { get => deltaTime; private set => deltaTime = value; }

        public static event Action UpdataAction;

        private FrameUpdater() { }
    }
}