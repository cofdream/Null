using UnityEngine;

namespace NullNamespace
{
    public class ResourcesrLoad : IAssetLoad
    {
        private string name;
        public string Name { get { return name; } }

        public Object Asset { get; private set; }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<IAssetLoad> OnUnload;

        private ResourceRequest resourceRequest;

        private string assetPath;

        public AssetLoadState LoadState { get; private set; }

        private System.Type loadType;

        public ResourcesrLoad(string path, System.Type loadType)
        {
            referenceCount = 0;

            name = path;
            assetPath = path.Substring("resources://".Length);

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;
        }
        public void LoadAsset()
        {
            if (LoadState == AssetLoadState.NotLoad)
            {
                LoadState = AssetLoadState.Loading;

                Asset = Resources.Load(this.assetPath, loadType);
                LoadState = AssetLoadState.Loaded;
            }
        }

        public void LoadAssetSync(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:
                    LoadState = AssetLoadState.Loading;
                    resourceRequest = Resources.LoadAsync(assetPath, loadType);
                    resourceRequest.completed += ResourceRequest_completed;

                    OnLoaded += onLoaded;
                    break;
                case AssetLoadState.Loading:
                    OnLoaded += onLoaded;
                    break;
                case AssetLoadState.Loaded:
                    onLoaded?.Invoke(Asset);
                    break;
            }
        }

        private void ResourceRequest_completed(AsyncOperation obj)
        {
            LoadState = AssetLoadState.Loaded;

            Asset = resourceRequest.asset;
            resourceRequest = null;

            OnLoaded?.Invoke(Asset);
            OnLoaded = null;
        }

        public void Retain()
        {
            referenceCount++;
        }
        public void Release()
        {
            referenceCount--;

            if (referenceCount == 0)
            {
                LoadState = AssetLoadState.Unload;

                if (Asset is GameObject == false)
                    Resources.UnloadAsset(Asset);
                //Resources.UnloadUnusedAssets();
                Asset = null;

                OnUnload?.Invoke(this);

                OnUnload = null;
            }
        }
    }
}