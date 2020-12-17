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
        private string assetName;
        private ushort refNumber = 0;

        private AssetLoadState loadState;

        public event Action<IAssetLoad> UnloadCallBack;
        public LocalAssetLoad()
        {
            Reset();
        }
        private void Reset()
        {
            asset = null;
            assetPath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;
            UnloadCallBack = null;
        }

        public bool Equals(string path, string name)
        {
            return assetPath.Equals(path) && assetName.Equals(name);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.asset.Equals(asset);
        }
        public T LoadAsset<T>(string path, string name) where T : UnityEngine.Object
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetPath = path;
                assetName = name;
                loadState = AssetLoadState.Loading;

                asset = AssetDatabase.LoadAssetAtPath<T>($"{path}/{assetName}");

                loadState = AssetLoadState.Loaded;
            }

            return Load() as T;
        }
        public void LoadAsync<T>(string path, string name, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:

                    assetPath = path;
                    assetName = name;
                    loadState = AssetLoadState.Loading;

                    asset = AssetDatabase.LoadAssetAtPath<T>($"{path}/{assetName}");

                    Timer.Timer.timers.Add(new Timer.TimerOnce()
                    {
                        ElapsedTime = 0,
                        TotalTime = 0.1f,
                        CallBack = () =>
                        {
                            loadState = AssetLoadState.Loaded;

                            loadCallBack?.Invoke(Load() as T);
                        },
                    });

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load() as T);

                    break;
            }
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
                loadState = AssetLoadState.Unload;
                UnloadCallBack.Invoke(this);
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