using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DA
{
    public class AssetManager
    {
        public static List<AssetLoader> loaders = new List<AssetLoader>();
    }

    public class AssetLoader
    {
        private List<IAsset> assets = new List<IAsset>();

        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                AssetManager.loaders.Add(loader);
                return loader;
            }
        }

        private IAsset GetAsset<T>(string path) where T : UnityEngine.Object
        {
            foreach (var item in assets)
            {
                if (item.Equals(path))
                {
                    return item;
                }
            }

            var loders = AssetManager.loaders;
            foreach (var loader in loders)
            {
                //if (loader == this)
                //    continue; //减少重复检查

                foreach (var item in loader.assets)
                {
                    if (item.Equals(path))
                    {
                        assets.Add(item);//全局内的资源 保存本地一份
                        return item;
                    }
                }
            }

#if UNITY_EDITOR
            IAsset asset = new AssetAB();// Asset();
#else
            IAsset asset = new AssetAB();
#endif
            assets.Add(asset);
            return asset;
        }
        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return GetAsset<T>(path).LoadAsset<T>(path);
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            GetAsset<T>(path).LoadAsync<T>(path, loadCallBack);
        }
        public void Unload(UnityEngine.Object asset)
        {
            int length = assets.Count;
            for (int i = 0; i < length; i++)
            {
                IAsset _asset = assets[i];
                if (_asset.Equals(asset))
                {
                    _asset.Unload();
                    assets.RemoveAt(i);
                    break;
                }
            }
        }
        public void UnloadAll()
        {
            foreach (var asset in assets)
            {
                asset.Unload();
            }
            assets.Clear();
        }

        public void Dispose()
        {
            if (assets.Count == 0)
            {
                AssetManager.loaders.Remove(this);
            }
        }
    }

#if UNITY_EDITOR
    public class Asset : IAsset
    {
        private UnityEngine.Object asset;
        private string assetPath;
        private ushort refNumber = 0;

        private bool isLoaded;

        public Asset()
        {
            Reset();
        }
        private void Reset()
        {
            asset = null;
            assetPath = null;
            refNumber = 0;
            isLoaded = false;
        }

        public bool Equals(string path)
        {
            return assetPath.Equals(path);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.asset.Equals(asset);
        }
        public T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            if (asset == null)
            {
                if (isLoaded) return null; // throw new Exception("当前Asset 已加载过,不要重复加载");

                isLoaded = true;
                assetPath = path;
                asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);


                AssetDatabase.LoadAssetAtPath<Sprite>(path);
            }
            return Load() as T;
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            if (asset != null)
            {
                T temp = Load() as T;
                loadCallBack?.Invoke(temp);
                return;
            }
            if (isLoaded) return; // throw new Exception("当前Asset 已加载过,不要重复加载");

            isLoaded = true;

            assetPath = path;
            asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            //todo 基于携程 实现一个异步 
            Timer.Timer.timers.Add(new Timer.TimerOnce()
            {
                CallBack = () =>
                {
                    T temp = Load() as T;
                    loadCallBack?.Invoke(temp);
                },
                ElapsedTime = 0,
                TotalTime = 0.3f,
            });
        }
        private UnityEngine.Object Load()
        {
            refNumber++;
            return asset;
        }
        public void Unload()
        {
            refNumber--;
            if (refNumber == 0)
            {
                if (asset is GameObject)
                {
                    Resources.UnloadUnusedAssets();
                }
                else
                {
                    Resources.UnloadAsset(asset);
                }
            }
        }
    }
#endif
    public class AssetAB : IAsset
    {
        private AssetBundle assetBundle;
        private string assetPath;
        private ushort refNumber;

        private AssetBundleCreateRequest createRequest;
        private Action<UnityEngine.Object> onLoaded;

        private bool isLoaded;

        public AssetAB()
        {
            Reset();
        }

        private void Reset()
        {
            assetBundle = null;
            assetPath = null;
            refNumber = 0;
            createRequest = null;
            onLoaded = null;
            isLoaded = false;
        }

        public bool Equals(string path)
        {
            return assetPath.Equals(path);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.assetBundle.Equals(asset);
        }
        public T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            if (assetBundle == null)
            {
                if (isLoaded)
                    return null; //throw new Exception("当前Asset 已加载过,不要重复加载");

                isLoaded = true;

                assetPath = path;
                assetBundle = AssetBundle.LoadFromFile(path);
            }


            return Load() as T;
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            if (assetBundle != null)
            {
                T temp = Load() as T;
                loadCallBack?.Invoke(temp);
                return;
            }
            Delegate va = loadCallBack;

            va.DynamicInvoke();

            if (isLoaded)
                return;//throw new Exception("当前Asset 已加载过,不要重复加载");
            isLoaded = true;

            assetPath = path;
            createRequest = AssetBundle.LoadFromFileAsync(path);
            createRequest.completed += ABLoadCallBack;
        }
        private void ABLoadCallBack(AsyncOperation asyncOperation)
        {
            assetBundle = createRequest.assetBundle;
            var temp = Load();
            onLoaded?.Invoke(temp);

            createRequest = null;
        }

        private UnityEngine.Object Load()
        {
            refNumber++;
            return assetBundle;
        }

        public void Unload()
        {
            refNumber--;
            if (refNumber == 0)
            {
                assetBundle.Unload(true);
            }
        }
    }

    interface IAsset
    {
        bool Equals(string path);
        bool Equals(UnityEngine.Object asset);
        T LoadAsset<T>(string path) where T : UnityEngine.Object;
        void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object;
        void Unload();
    }
}