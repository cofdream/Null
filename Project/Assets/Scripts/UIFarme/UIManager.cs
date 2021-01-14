using DA.DataConfig;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA.UI
{
    public class UIManager
    {
        private static Dictionary<Type, UIWindowBase> windowCache = null;

        public static DialogControll DialogManager { get; private set; }

        private static UIRootBind uiRootBind;

        private const string UIRootPath = "Resources/Prefabs/UI/UI Root";
        static UIManager()
        {
            //#if UNITY_EDITOR
            //            AssetLoad.AssetLoadManager.IsSimulationMode = false;
            //#endif
            var uiRoot = AssetLoad.AssetLoader.GetAssetLoader().LoadAsset<GameObject>(UIRootPath);
            uiRoot = GameObject.Instantiate(uiRoot);
            uiRootBind = uiRoot.GetComponent<UIRootBind>();
            UnityEngine.Object.DontDestroyOnLoad(uiRoot);

            windowCache = new Dictionary<Type, UIWindowBase>(30);
            DialogManager = new DialogControll();
        }

        public static Event.IDispatch OpenWindow(string windowName)
        {
            var config = DataConfigManager.GetUIWindowConfigByName(windowName);

            var type = Type.GetType(config.ClassName);

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


        private static T GetWindow<T>() where T : UIWindowBase, new()
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

        private static T CreateWindow<T>(DAProto.UI_Config config) where T : UIWindowBase, new()
        {
            var window = new T()
            {
                Config = config,
                BindBase = CreateWindowContext(config.PrefabPath).GetComponent<UIBindBase>(),
            };
            return window;
        }

        private static GameObject CreateWindowContext(string path)
        {
            var gameObject = CreateWindowPrefab(path);
            gameObject = GameObject.Instantiate(gameObject, uiRootBind.UIFullScreenLayer);
            gameObject.transform.SetAsLastSibling();
            return gameObject;
        }

        private static GameObject CreateWindowPrefab(string path)
        {
            GameObject win = AssetLoad.AssetLoader.GetAssetLoader().LoadAsset<GameObject>(path);
            return win;
        }


        public static void ShowWin(string winName)
        {
            Event.IDispatch dispatch;
            ShowWin(winName, out dispatch);
        }
        public static void ShowWin(string winName, out Event.IDispatch dispatch)
        {
            var config = DataConfigManager.GetUIWindowConfigByName(winName);

            var uiType = Type.GetType(config.ClassName);

            var window = Activator.CreateInstance(uiType) as UIWindowBase;
            windowCache.Add(uiType, window);

            window.Config = config;

            //GameObject bind = CreateWindowContext(config.PrefabPath);
            //window.BindBase = bind.GetComponent<UIBindBase>();

            window.OnInit();

            dispatch = window;
        }
    }
}