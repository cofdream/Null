﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA
{
    public partial class AssetLoader
    {
        private List<IAssetLoad> assetLoads = new List<IAssetLoad>();
        private List<IAssetLoad> refAssetLoads = new List<IAssetLoad>();

        private IAssetLoad GetAsset(string assetName, string assetPath)
        {
            // 本地的Load
            foreach (var item in assetLoads)
            {
                if (item.Equals(assetPath))
                {
                    return item;
                }
            }

            // 本地引用的外部Load
            foreach (var item in refAssetLoads)
            {
                if (item.Equals(assetPath))
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
                    if (item.Equals(assetPath))
                    {
                        item.UnloadCallback += RemoveRefLoader;
                        refAssetLoads.Add(item);//全局内的资源 保存本地一份
                        return item;
                    }
                }
            }

            IAssetLoad assetLoad;
#if UNITY_EDITOR
            if (IsSimulationMode == false)
                assetLoad = new AssetEditorLoad().Init();
            else
#endif
            {
                if (assetPath.EndsWith(".bundle"))
                    assetLoad = new AssetBundleLoad().Init(this);
                else
                    assetLoad = new AssetLoad().Init();
            }

            assetLoad.UnloadCallback += RemoveLoader;
            assetLoads.Add(assetLoad);

            return assetLoad;
        }

        public T Load<T>(string assetName, string assetPath) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetAsset(assetName, assetPath);

#if UNITY_EDITOR
            var local = assetLoad as AssetEditorLoad;
            if (local != null) local.LoadType = typeof(T);
#endif
            return assetLoad.LoadAsset(assetPath) as T;
        }
        public void LoadAsync<T>(string assetName, string assetPath, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetAsset(assetName, assetPath);

#if UNITY_EDITOR
            var local = assetLoad as AssetEditorLoad;
            if (local != null) local.LoadType = typeof(T);
#endif

            assetLoad.LoadAsync(assetPath, (asset) =>
            {
                 loadCallBack?.Invoke(asset as T);
            });
        }

        public void Unload(UnityEngine.Object asset)
        {
            int length = assetLoads.Count;
            for (int i = 0; i < length; i++)
            {
                IAssetLoad assetload = assetLoads[i];
                if (assetload.Equals(asset))
                {
                    assetload.UnloadCallback -= RemoveLoader;//移除回调
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
                    assetload.UnloadCallback -= RemoveRefLoader;//移除回调
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
                assetload.UnloadCallback -= RemoveLoader;//移除回调
                assetload.Unload();
            }
            assetLoads.Clear();

            foreach (var assetload in refAssetLoads)
            {
                assetload.UnloadCallback -= RemoveRefLoader;//移除回调
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
            assetLoad.UnloadCallback -= RemoveLoader;
            assetLoads.Remove(assetLoad);
        }
        private void RemoveRefLoader(IAssetLoad assetLoad)
        {
            assetLoad.UnloadCallback -= RemoveRefLoader;
            refAssetLoads.Remove(assetLoad);
        }
    }
}