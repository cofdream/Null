using DA.Event;
using DAProto;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace DA.UI
{
    public abstract class UIWindowBase : Event.IDispatch
    {
        public UIBindBase BindBase;

        public UI_Config Config;

        public UnityAction DisposeCallBack { protected get; set; }

        protected EventDispatcher eventDispatcher;
        public IDispatcher Dispatcher { get { return eventDispatcher; } }

        public virtual void OnInit()
        {
            eventDispatcher = new EventDispatcher();

            string fileNmae = Path.GetFileName(Config.PrefabPath);

            //resLoader.Add2Load(fileNmae, (arg1, arg2) => {
            //    if (arg1)
            //    {
            //        GameObject go = arg2.Asset.As<GameObject>().Instantiate();

            //        BindBase = go.GetComponent<UIBindBase>();

            //        OnOpen();

            //        resLoader.Dispose();
            //    }
            //});

            //resLoader.LoadAsync();
        }

        public virtual void OnDestory()
        {
        }
        public virtual void OnOpen()
        {
            Debug.Log(BindBase.name + " Init Done.");
        }
        public virtual void OnClose()
        {

        }
    }
}