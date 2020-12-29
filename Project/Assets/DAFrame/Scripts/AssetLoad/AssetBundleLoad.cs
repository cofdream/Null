using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetBundleLoad : IAssetLoad
    {
        private static System.Type loadType = typeof(AssetBundle);

        private string name;
        public string Name { get { return name; } }

        private AssetBundle assetBundle;
        public Object Asset { get => assetBundle; }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<string> OnUnload;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private int loadedDirectDependentCount;
        private string[] directDependentArray;


        private System.Action<string> loadAssetAsync;
        private System.Action<string, System.Action> loadAssetSync;

        public AssetLoadState LoadState { get; private set; }

        public AssetBundleLoad(string path, System.Action<string> loadAssetAsync, System.Action<string, System.Action> loadAssetSync)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadAssetAsync = loadAssetAsync;
            this.loadAssetSync = loadAssetSync;
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundle = AssetBundle.LoadFromFile(AssetBundleConfig.AssetBundleRoot + assetPath);

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
                    Debug.LogError("资源已卸载，无法加载。");
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

                    assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(AssetBundleConfig.AssetBundleRoot + assetPath);

                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));

                    foreach (var directDependent in directDependentArray)
                    {
                        loadAssetSync?.Invoke(directDependent, DirectDependentCallback);
                    }

                    assetBundleCreateRequest.completed += ResourceRequest_completed;
                    OnLoaded += onLoaded;

                    break;
                case AssetLoadState.Loading:
                case AssetLoadState.Loaded:

                case AssetLoadState.LoadError:

                    OnLoaded += onLoaded;

                    break;
                case AssetLoadState.Unload:
                    Debug.LogError("资源已卸载，无法加载。");
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
        private void ResourceRequest_completed(AsyncOperation obj)
        {
            assetBundle = assetBundleCreateRequest.assetBundle;

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

                foreach (var dependent in directDependentArray)
                {
                    OnUnload(dependent);
                }

                OnUnload(name);
                OnUnload = null;

                assetBundle.Unload(true);
                assetBundle = null;
            }
        }
    }
}