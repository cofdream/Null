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

        public event Action<IAssetLoad> UnloadCallback;

        public IAssetLoad Init()
        {
            assetBundle = null;
            assetBundlePath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;

            UnloadCallback = null;

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
        public UnityEngine.Object LoadAsset(string assetPath)
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetBundlePath = assetPath;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

                loadState = AssetLoadState.Loaded;
            }

            return Load();
        }
        public void LoadAsync(string assetPath, Action<UnityEngine.Object> loadCallBack)
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

                        loadCallBack?.Invoke(Load());
                    };

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load());

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
                UnloadCallback.Invoke(this);
                assetBundle.Unload(true);
            }
        }
    }
}