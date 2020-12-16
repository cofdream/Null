using System;
using System.Collections.Generic;

namespace DA
{
    public class AssetLoader
    {
#if UNITY_EDITOR
        public static bool IsSimulationMode = true;
#endif
        private static List<AssetLoader> loaders = new List<AssetLoader>();


        private List<IAssetLoad> assets = new List<IAssetLoad>();

        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                loaders.Add(loader);
                return loader;
            }
        }

        private IAssetLoad GetAsset<T>(string path) where T : UnityEngine.Object
        {
            foreach (var item in assets)
            {
                if (item.Equals(path))
                {
                    return item;
                }
            }

            foreach (var loader in loaders)
            {
                if (loader == this)
                    continue; //减少重复检查

                foreach (var item in loader.assets)
                {
                    if (item.Equals(path))
                    {
                        assets.Add(item);//全局内的资源 保存本地一份
                        return item;
                    }
                }
            }

            IAssetLoad asset;
#if UNITY_EDITOR
            if (IsSimulationMode == false)
                asset = new LocalAssetLoad();
            else
#endif
                asset = new AssetBundleLoad();

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
                IAssetLoad _asset = assets[i];
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
                loaders.Remove(this);
            }
        }
    }
}