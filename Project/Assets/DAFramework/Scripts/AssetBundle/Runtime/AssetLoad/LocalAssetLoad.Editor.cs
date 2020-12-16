#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace DA
{
    public class LocalAssetLoad : IAssetLoad
    {
        private UnityEngine.Object asset;
        private string assetPath;
        private ushort refNumber = 0;

        private bool isLoaded;

        public LocalAssetLoad()
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
}
#endif