#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace DA
{
    public class AssetEditorLoad : IAssetLoad
    {
        private UnityEngine.Object asset;
        private string assetPath;
        private ushort refNumber = 0;

        private AssetLoadState loadState;

        public Type LoadType;

        public event Action<IAssetLoad> UnloadCallback;

        public IAssetLoad Init()
        {
            asset = null;
            assetPath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;
            UnloadCallback = null;

            return this;
        }

        public bool Equals(string assetPath, string assetName)
        {
            return this.assetPath.Equals(assetPath);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.asset.Equals(asset);
        }
        public UnityEngine.Object LoadAsset(string assetPath, string assetName)
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                this.assetPath = assetPath;
                loadState = AssetLoadState.Loading;

                asset = AssetDatabase.LoadAssetAtPath(this.assetPath, LoadType);

                loadState = AssetLoadState.Loaded;
            }

            return Load();
        }
        public void LoadAsync(string assetPath, string assetName, Action<UnityEngine.Object> loadCallBack)
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:

                    this.assetPath = assetPath;
                    loadState = AssetLoadState.Loading;

                    asset = AssetDatabase.LoadAssetAtPath(this.assetPath, LoadType);

                    //Timer.Timer.timers.Add(new Timer.TimerOnce()
                    //{
                    //    ElapsedTime = 0,
                    //    TotalTime = 0.1f,
                    //    Callback= () =>
                    //    {
                    //        loadState = AssetLoadState.Loaded;

                    //        loadCallBack?.Invoke(Load());
                    //    },
                    //});

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load());

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
                UnloadCallback.Invoke(this);
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