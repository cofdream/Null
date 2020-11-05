using UnityEngine;

namespace DA.UI
{
    public interface IUIWindow
    {
        void Awake();
        void OnEnable();
        void OnDisable();
        void OnDestory();
        Object Context { set; }
    }
}