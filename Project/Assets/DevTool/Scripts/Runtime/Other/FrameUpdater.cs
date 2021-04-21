using System;
using UnityEngine;

namespace DA
{
    [DisallowMultipleComponent]
    public static class FrameUpdater
    {
        private static float deltaTime;
        public static float DeltaTime { get => deltaTime; private set => deltaTime = value; }

        public static event Action UpdataAction;

        static FrameUpdater()
        {
            UnityEngine.Object.DontDestroyOnLoad(new GameObject("FrameUpdater (instance)").AddComponent<FrameUpdaterComponent>());
        }


        [DisallowMultipleComponent]
        private sealed class FrameUpdaterComponent : MonoBehaviour
        {
            private void Update()
            {
                deltaTime = Time.deltaTime;

                UpdataAction?.Invoke();
            }
        }
    }
}
