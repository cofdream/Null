using UnityEngine;
using UnityEngine.Events;

namespace DA.UI
{
    public abstract class UIWindow
    {
        public virtual GameObject Context { set { } }
        public bool Enable;
        public virtual void Awake()
        {
        }
        public virtual void OnDestory()
        {
        }
        public virtual void OnEnable(object age)
        {
        }
        public virtual void OnDisable()
        {
        }
        public virtual void SetContext(GameObject context)
        {
        }
    }
}