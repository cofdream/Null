using DA.DataConfig;
using DA.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Transform windowsTran = null;

        Dictionary<Type, UIWindowBase> windowCache = null;

        public DialogControll DialogManager { get; private set; }

        protected override void InitSingleton()
        {
            windowsTran = GameObject.Find("UIRoot/Canvas").transform;
            windowCache = new Dictionary<Type, UIWindowBase>(30);
        }

        UIManager() { }
        public void Init()
        {
            Debug.Log("UIManager Init Done.");
            DialogManager = new DialogControll();
        }


        public Event.IDispatch OpenWindow(string windowName)
        {
            var config = DataConfigManager.GetUIWindowConfigByName(windowName);

            var type = Type.GetType("DA.UI." + config.ClassName);

            var window = Activator.CreateInstance(type) as UIWindowBase;
            windowCache.Add(type, window);

            window.Config = config;

            var bind = CreateWindowContext(config.PrefabPath);
            window.BindBase = bind.GetComponent<UIBindBase>();
            window.DisposeCallBack =
                () =>
                {
                    windowCache.Remove(type);
                };

            window.OnInit();

            window.OnOpen();

            return window;
        }


        private T GetWindow<T>() where T : UIWindowBase, new()
        {
            UIWindowBase window;
            Type type = typeof(T);
            if (windowCache.TryGetValue(type, out window))
            {
                return window as T;
            }
            else
            {
                var config = DataConfigManager.GetUIWindowConfigByType(type.Name);

                return CreateWindow<T>(config);
            }
        }

        private T CreateWindow<T>(DAProto.UIWindow_Config config) where T : UIWindowBase, new()
        {
            var window = new T()
            {
                Config = config,
                BindBase = CreateWindowContext(config.PrefabPath).GetComponent<UIBindBase>(),
            };
            return window;
        }

        private GameObject CreateWindowContext(string path)
        {
            var gameObject = CreateWindowPrefab(path);
            gameObject = GameObject.Instantiate(gameObject, windowsTran);
            gameObject.transform.SetAsLastSibling();
            return gameObject;
        }

        private GameObject CreateWindowPrefab(string path)
        {
            GameObject win = Resources.Load<GameObject>(path);
            return win;
        }

    }
}