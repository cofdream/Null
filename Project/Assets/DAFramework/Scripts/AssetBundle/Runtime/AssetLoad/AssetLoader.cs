using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA
{
    public class AssetLoader
    {
#if UNITY_EDITOR
        public static bool IsSimulationMode = true;
#endif
        private static List<AssetLoader> loaders = new List<AssetLoader>();


        private List<IAssetLoad> assetLoads = new List<IAssetLoad>();
        private List<IAssetLoad> refAssetLoads = new List<IAssetLoad>();

        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                loaders.Add(loader);
                return loader;
            }
        }

        private IAssetLoad GetAsset<T>(string path, string name) where T : UnityEngine.Object
        {
            // 本地的Load
            foreach (var item in assetLoads)
            {
                if (item.Equals(path, name))
                {
                    return item;
                }
            }

            // 本地引用的外部Load
            foreach (var item in refAssetLoads)
            {
                if (item.Equals(path, name))
                {
                    return item;
                }
            }

            // 外部Loader中遍历Load
            foreach (var loader in loaders)
            {
                // 也许会成为负优化（在单个loader里加载的资源偏少、loaders偏多的情况下）
                //if (loader == this)
                //    continue; //剔除自己 减少重复检查

                // 仅检测自己创建的
                foreach (var item in loader.assetLoads)
                {
                    if (item.Equals(path, name))
                    {
                        item.UnloadCallBack += RemoveRefLoader;
                        refAssetLoads.Add(item);//全局内的资源 保存本地一份
                        return item;
                    }
                }
            }

            IAssetLoad assetLoad;
#if UNITY_EDITOR
            if (IsSimulationMode == false)
                assetLoad = new LocalAssetLoad();
            else
#endif
                assetLoad = new AssetBundleLoad();

            assetLoad.UnloadCallBack += RemoveLoader;
            assetLoads.Add(assetLoad);

            return assetLoad;
        }
        public T Load<T>(string path, string name) where T : UnityEngine.Object
        {
            return GetAsset<T>(path, name).LoadAsset<T>(path, name);
        }
        public void LoadAsync<T>(string path, string name, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            GetAsset<T>(path, name).LoadAsync<T>(path, name, loadCallBack);
        }
        public void Unload(UnityEngine.Object asset)
        {
            int length = assetLoads.Count;
            for (int i = 0; i < length; i++)
            {
                IAssetLoad assetload = assetLoads[i];
                if (assetload.Equals(asset))
                {
                    assetload.UnloadCallBack -= RemoveLoader;//移除回调
                    assetload.Unload();
                    assetLoads.RemoveAt(i);
                    return;
                }
            }

            length = refAssetLoads.Count;
            for (int i = 0; i < length; i++)
            {
                IAssetLoad assetload = refAssetLoads[i];
                if (assetload.Equals(asset))
                {
                    assetload.UnloadCallBack -= RemoveRefLoader;//移除回调
                    assetload.Unload();
                    refAssetLoads.RemoveAt(i);
                    return;
                }
            }

            Dispose();
        }

        public void UnloadAll()
        {
            foreach (var assetload in assetLoads)
            {
                assetload.UnloadCallBack -= RemoveLoader;//移除回调
                assetload.Unload();
            }
            assetLoads.Clear();

            foreach (var assetload in refAssetLoads)
            {
                assetload.UnloadCallBack -= RemoveRefLoader;//移除回调
                assetload.Unload();
            }
            refAssetLoads.Clear();

            Dispose();
        }

        public void Dispose()
        {
            if (assetLoads.Count == 0)
            {
                loaders.Remove(this);
            }
        }


        private void RemoveLoader(IAssetLoad assetLoad)
        {
            assetLoad.UnloadCallBack -= RemoveLoader;
            assetLoads.Remove(assetLoad);
        }
        private void RemoveRefLoader(IAssetLoad assetLoad)
        {
            assetLoad.UnloadCallBack -= RemoveRefLoader;
            refAssetLoads.Remove(assetLoad);
        }
    }
}