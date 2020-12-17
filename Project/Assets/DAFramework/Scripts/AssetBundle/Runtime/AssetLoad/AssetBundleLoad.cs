using System;
using UnityEngine;

namespace DA
{
    public class AssetBundleLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private Delegate onLoad;
        private string assetBundlePath;
        private string assetName;
        private ushort refNumber;

        private AssetBundleCreateRequest createRequest;

        private AssetLoadState loadState;

        public event Action<IAssetLoad> UnloadCallBack;

        public AssetBundleLoad()
        {
            Reset();
        }

        private void Reset()
        {
            assetBundle = null;
            assetBundlePath = null;
            refNumber = 0;
            createRequest = null;
            onLoad = null;
            loadState = AssetLoadState.NotLoaded;
            UnloadCallBack = null;
        }

        public bool Equals(string path, string name)
        {
            return assetBundlePath.Equals(path)
                && (loadState == AssetLoadState.Loading || loadState == AssetLoadState.Loaded);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.assetBundle.Equals(asset);
        }
        public T LoadAsset<T>(string path, string name) where T : UnityEngine.Object
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetBundlePath = path;
                assetName = name;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile($"{path}/{assetName}");

                loadState = AssetLoadState.Loaded;
            }

            return Load() as T;
        }
        public void LoadAsync<T>(string path, string name, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:

                    assetBundlePath = path;
                    assetName = name;
                    loadState = AssetLoadState.Loading;

                    createRequest = AssetBundle.LoadFromFileAsync($"{path}/{assetName}");
                    createRequest.completed += ABLoadCallBack;

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load() as T);

                    break;
            }
        }
        private void ABLoadCallBack(AsyncOperation asyncOperation)
        {
            loadState = AssetLoadState.Loaded;

            assetBundle = createRequest.assetBundle;

            onLoad?.DynamicInvoke(Load());

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
                loadState = AssetLoadState.Unload;
                UnloadCallBack.Invoke(this);
                assetBundle.Unload(true);
            }
        }
    }
}