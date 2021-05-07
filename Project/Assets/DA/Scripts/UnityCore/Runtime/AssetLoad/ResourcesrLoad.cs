using UnityEngine;

#if UNITY_EDITOR
namespace DA.AssetLoad
{
    public class ResourcesrLoad : IAssetLoad
    {
        private static ObjectPool.ObjectPool<ResourcesrLoad> resourceLoadPool;

        private string name;
        public string Name { get { return name; } }
        public Object Asset { get; private set; }
        public AssetLoadState LoadState { get; private set; }

        private int referenceCount;

        private System.Action<Object> onLoaded;

        private ResourceRequest resourceRequest;

        private string assetPath;

        private System.Type loadType;

        private System.Action<string> unloadCallback;

        static ResourcesrLoad()
        {
            resourceLoadPool = new ObjectPool.ObjectPool<ResourcesrLoad>();
            resourceLoadPool.Initialize(() => new ResourcesrLoad(), null, null, null);
        }
        public static ResourcesrLoad GetLoad(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            var load = resourceLoadPool.Allocate();
            load.Initialize(path, loadType, unloadCallback);
            return load;
        }

        public void Initialize(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            referenceCount = 0;

            name = path;
            assetPath = path.Substring("resources://".Length);

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;

            this.unloadCallback = unloadCallback;
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
                    resourceRequest.completed += ResourceRequestCompleted;

                    this.onLoaded += onLoaded;
                    break;
                case AssetLoadState.Loading:
                    this.onLoaded += onLoaded;
                    break;
                case AssetLoadState.Loaded:
                    onLoaded?.Invoke(Asset);
                    break;
            }
        }

        private void ResourceRequestCompleted(AsyncOperation obj)
        {
            LoadState = AssetLoadState.Loaded;

            Asset = resourceRequest.asset;
            resourceRequest = null;

            onLoaded?.Invoke(Asset);
            onLoaded = null;
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

                unloadCallback?.Invoke(Name);

                Clear();

                resourceLoadPool.Release(this);
            }
        }
        private void Clear()
        {
            name = null;
            Asset = null;
            assetPath = null;
            loadType = null;
            unloadCallback = null;
        }
    }
}
#endif