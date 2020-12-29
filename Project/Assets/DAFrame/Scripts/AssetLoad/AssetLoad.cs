using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetLoad : IAssetLoad
    {
        private static System.Type typeAB = typeof(AssetBundle);

        private string name;
        public string Name { get { return name; } }

        private Object asset;
        public Object Asset { get { return asset; } }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<string> OnUnload;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private int loadedDirectDependentCount;
        private string[] directDependentArray;


        private System.Action<string> loadAssetAsync;
        private System.Action<string, System.Action> loadAssetSync;

        private System.Type loadType;


        public AssetLoadState LoadState { get; private set; }

        public AssetLoad(string path, System.Action<string> loadAssetAsync, System.Action<string, System.Action> loadAssetSync, System.Type loadType)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadAssetAsync = loadAssetAsync;
            this.loadAssetSync = loadAssetSync;

            this.loadType = loadType;
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    var assetBundle = AssetBundle.LoadFromFile(ABManifest.GetAssetBundleByAssetPath(assetPath));

                    if (loadType.Equals(typeAB))
                    {
                        asset = assetBundle.LoadAsset(this.assetPath, loadType);
                    }

                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));
                    foreach (var directDependent in directDependentArray)
                    {
                        loadAssetAsync?.Invoke(directDependent);
                    }

                    LoadState = AssetLoadState.Loaded;

                    break;
                case AssetLoadState.Loading:
                    Debug.LogError("资源已在异步加载队列中，无法立即加载。AssetPath:" + this.assetPath);
                    break;
                case AssetLoadState.Loaded:
                    break;
                case AssetLoadState.Unload:
                    Debug.LogError("资源已卸载，任在加载。");
                    break;
                case AssetLoadState.LoadError:
                    break;
            }
        }

        public void LoadAssetSync(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(ABManifest.GetAssetBundleByAssetPath(assetPath));

                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));

                    foreach (var directDependent in directDependentArray)
                    {
                        loadAssetSync.Invoke(directDependent, DirectDependentCallback);
                    }
                    OnLoaded += onLoaded;

                    break;
                case AssetLoadState.Loading:
                case AssetLoadState.Loaded:

                    OnLoaded += onLoaded;

                    break;
                case AssetLoadState.Unload:
                    Debug.LogError("资源已卸载，任在加载。");
                    break;
            }
        }

        private void DirectDependentCallback()
        {
            loadedDirectDependentCount++;
            if (loadedDirectDependentCount == directDependentArray.Length)
            {
                assetBundleCreateRequest.completed += ResourceRequest_completed;
                // 释放部分引用
                loadAssetSync = null;
                loadAssetAsync = null;
            }
        }
        private void ResourceRequest_completed(AsyncOperation asyncOperation)
        {
            LoadState = AssetLoadState.Loaded;

            var assetBundle = assetBundleCreateRequest.assetBundle;
            if (loadType.Equals(typeAB))
            {
                asset = assetBundle.LoadAsset(this.assetPath, loadType);
            }
            assetBundleCreateRequest = null;

            OnLoaded?.Invoke(asset);
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

                foreach (var item in directDependentArray)
                {
                    OnUnload(item);
                }

                OnUnload(name);
                OnUnload = null;

                var assetBundle = asset as AssetBundle;
                if (assetBundle != null)
                    assetBundle.Unload(true);

                asset = null;
            }
        }
    }
}