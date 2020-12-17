using System;
using UnityEngine;

namespace DA
{
    public class AssetLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private string assetBundlePath;
        private ushort refNumber;
        private AssetLoadState loadState;

        public event Action<IAssetLoad> UnloadCallBack;

        public IAssetLoad Init()
        {
            assetBundle = null;
            assetBundlePath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;

            UnloadCallBack = null;

            return this;
        }

        public bool Equals(string path)
        {
            return assetBundlePath.Equals(path);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return assetBundle.Equals(asset);
        }
        public T LoadAsset<T>(string assetPath) where T : UnityEngine.Object
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetBundlePath = assetPath;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

                loadState = AssetLoadState.Loaded;
            }

            return Load() as T;
        }
        public void LoadAsync<T>(string assetPath, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:
                    assetBundlePath = assetPath;
                    loadState = AssetLoadState.Loading;

                    // todo寻找依赖，把依赖文件都加载进来

                    var createRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
                    createRequest.completed += (AsyncOperation asyncOperation) =>
                    {
                        loadState = AssetLoadState.Loaded;

                        assetBundle = createRequest.assetBundle;

                        loadCallBack?.Invoke(Load() as T);
                    };

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load() as T);

                    break;
            }
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