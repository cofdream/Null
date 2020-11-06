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

        private BattlePanel battlePanel = null;

        Dictionary<string, UIWindow> windows = null;
        Dictionary<string, GameObject> windowGameObjs = null;

        #region Life
        protected override void SingletonInit()
        {
            windowsTran = GameObject.Find("UIRoot/Canvas").transform;
            windows = new Dictionary<string, UIWindow>(30);
            windowGameObjs = new Dictionary<string, GameObject>(30);
        }
        public override void Free()
        {

        }
        #endregion

        UIManager() { }
        public void Init()
        {
            Debug.Log("UIManager Init Done.");
        }

        public void OpenWindow<T>(string windowName) where T : UIWindow, new()
        {
            GameObject windowGameObj = null;
            UIWindow window = null;
            if (windows.TryGetValue(windowName, out window))
            {
                windowGameObj = windowGameObjs[windowName];
            }

            var window_Config = DataConfigManager.GetUIWindowConfig(windowName);
            T window = CreateWindow<T>(window_Config.PrefabPath);
        }

        private T CreateWindow<T>(string path)
        {
            var windowGameObj = CreateWindowPrefab(path);

            windowGameObj = GameObject.Instantiate(windowGameObj, windowsTran);

            T t = new T();
            t.SetContext(windowGameObj);

        }


        private GameObject CreateWindowPrefab(string windowName)
        {
            GameObject win = Resources.Load<GameObject>($"Prefabs/UI/{windowName}/{windowName}");
            return win;
        }

    }
}