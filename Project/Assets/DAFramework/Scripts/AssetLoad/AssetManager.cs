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
        private List<Asset> assets = new List<Asset>();


        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                AssetManager.loaders.Add(loader);
                return loader;
            }
        }

        private Asset GetAsset<T>(string path) where T : UnityEngine.Object
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

            return null;
        }
        public T Load<T>(string path) where T : UnityEngine.Object
        {
            var asset = GetAsset<T>(path);
            if (asset == null)
            {
                asset = new Asset();
                assets.Add(asset);
                asset.LoadAsset<T>(path);
            }

            return asset.Load() as T;
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            var asset = GetAsset<T>(path);

            if (asset == null)
            {
                asset = new Asset();
                assets.Add(asset);
                asset.LoadAsync<T>(path, loadCallBack);
            }
            else
            {
                loadCallBack?.Invoke(asset.Load() as T);
            }
        }
        public void Unload(UnityEngine.Object asset)
        {
            int length = assets.Count;
            for (int i = 0; i < length; i++)
            {
                Asset _asset = assets[i];
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

    public class Asset
    {
        private UnityEngine.Object asset;

        private string assetPath;
        private ushort refNumber = 0;
        private Action<Asset> unusedAssetCallBack;

        public bool Equals(string path)
        {
            return assetPath.Equals(path);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.asset.Equals(asset);
        }
        public void LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            assetPath = path;
            asset = AssetDatabase.LoadAssetAtPath<T>(path);
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            if (asset != null)
            {
                loadCallBack?.Invoke(Load() as T);
            }

            assetPath = path;
            asset = AssetDatabase.LoadAssetAtPath<T>(path);
            loadCallBack?.Invoke(Load() as T);
        }
        public UnityEngine.Object Load()
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
}