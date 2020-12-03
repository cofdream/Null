using DA.Event;
using DAProto;
using QFramework;
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

            ResLoader resLoader = ResLoader.Allocate();

            string fileNmae = Path.GetFileName(Config.PrefabPath);

            GameObject go = resLoader.LoadSync<GameObject>(fileNmae);
            go = GameObject.Instantiate(go);
            resLoader.Dispose();

            BindBase = go.GetComponent<UIBindBase>();
            OnOpen();

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