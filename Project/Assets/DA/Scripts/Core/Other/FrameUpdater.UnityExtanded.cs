using UnityEngine;

namespace DA
{
    /// <summary>
    /// 使用unity函数来更新
    /// </summary>
    public partial class FrameUpdater
    {
        static FrameUpdater()
        {
            Object.DontDestroyOnLoad(new GameObject("FrameUpdater (instance)").AddComponent<FrameUpdaterComponent>());
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