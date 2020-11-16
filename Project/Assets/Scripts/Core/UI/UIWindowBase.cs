using DA.Event;
using DAProto;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DA.UI
{
    public abstract class UIWindowBase : Event.IDispatch
    {
        public UIBindBase BindBase;

        public UIWindow_Config Config;

        public UnityAction DisposeCallBack { protected get; set; }

        protected EventDispatcher eventDispatcher;
        public IDispatcher Dispatcher { get { return eventDispatcher; } }

        public virtual void OnInit()
        {
            eventDispatcher = new EventDispatcher();
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