using System;
using UnityEngine;

namespace DA
{
    public class AssetLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private string assetPath;
        private string assetName;
        private ushort refNumber;
        private AssetLoadState loadState;

        public event Action<IAssetLoad> UnloadCallback;

        public IAssetLoad Init()
        {
            assetBundle = null;
            assetPath = null;
            assetName = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;

            UnloadCallback = null;

            return this;
        }

        public bool Equals(string assetPath, string assetName)
        {
            return this.assetPath.Equals(assetPath) && this.assetName.Equals(assetName);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return assetBundle.Equals(asset);
        }
        public UnityEngine.Object LoadAsset(string assetPath, string assetName)
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                this.assetPath = assetPath;
                this.assetName = assetName;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile(this.assetName);

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
                    this.assetName = assetName;
                    loadState = AssetLoadState.Loading;

                    var createRequest = AssetBundle.LoadFromFileAsync(this.assetName);
                    // todo寻找依赖，把依赖文件都加载进来

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