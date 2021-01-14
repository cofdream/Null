using System;
using UnityEngine;

namespace DA
{
    [DisallowMultipleComponent]
    public static class FrameUpdater
    {
        private static float delateTime;
        public static float DelateTime { get => delateTime; private set => delateTime = value; }

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
                delateTime = Time.deltaTime;

                UpdataAction?.Invoke();
            }
        }

    }
}
