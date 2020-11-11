using DAProto;
using UnityEngine;

namespace DA.UI
{
    public abstract class UIWindowBase
    {
        public UIBindBase BindBase;

        public UIWindow_Config Config;


        public virtual void OnInit()
        {
        }
        public virtual void OnDestory()
        {
        }
        public virtual void OnOpen()
        {
        }
        public virtual void OnClose()
        {
        }
    }
}