using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetBundleLoad : IAssetLoad
    {
        private static ObjectPool.ObjectPool<AssetBundleLoad> assetBundleLoadPool;

        private string name;
        public string Name { get { return name; } }

        private AssetBundle assetBundle;
        public Object Asset { get => assetBundle; }

        private int referenceCount;

        private System.Action<Object> onLoaded;

        private System.Action<string> unloadCallback;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private int loadedDirectDependentCount;
        private int totalDirectDependecntCount;

        private AssetLoader assetLoader;

        public AssetLoadState LoadState { get; private set; }

        static AssetBundleLoad()
        {
            assetBundleLoadPool = new ObjectPool.ObjectPool<AssetBundleLoad>();
            assetBundleLoadPool.Initialize(() => new AssetBundleLoad(), null, null, null);
        }

        public static AssetBundleLoad GetAssetBundleLoad(string path, System.Action<string> unloadCallback)
        {
            var assetBundleLoad = assetBundleLoadPool.Allocate();
            assetBundleLoad.Initialize(path, unloadCallback);
            return assetBundleLoad;
        }

        public void Initialize(string path, System.Action<string> unloadCallback)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            assetLoader = AssetLoader.GetAssetLoader();

            this.unloadCallback = unloadCallback;
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundle = AssetBundle.LoadFromFile(AssetBundleConfig.AssetBundleRoot + assetPath);

                    var directDependencies = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));
                    totalDirectDependecntCount = directDependencies.Length;
                    foreach (var directDependent in directDependencies)
                    {
                        assetLoader.LoadAssetBundle(directDependent);
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

                    var directDependencies = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));
                    totalDirectDependecntCount = directDependencies.Length;
                    foreach (var directDependent in directDependencies)
                    {
                        assetLoader.LoadAssetBundleSync(directDependent, DirectDependentCallback);
                    }

                    this.onLoaded += onLoaded;

                    break;
                case AssetLoadState.Loading:
                case AssetLoadState.Loaded:

                case AssetLoadState.LoadError:

                    this.onLoaded += onLoaded;

                    break;
                case AssetLoadState.Unload:
                    Debug.LogError("资源已卸载，无法加载。");
                    break;
            }
        }
        private void DirectDependentCallback(Object loadAsstBundle)
        {
            loadedDirectDependentCount++;
            if (loadedDirectDependentCount == totalDirectDependecntCount)
            {
                assetBundleCreateRequest.completed += AssetBundleRequestCompleted;
            }
        }
        private void AssetBundleRequestCompleted(AsyncOperation obj)
        {
            assetBundle = assetBundleCreateRequest.assetBundle;
            assetBundleCreateRequest = null;

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

                //释放依赖
                assetLoader.UnloadAll();
                assetLoader.ReleaseAssetLoader();

                assetBundle.Unload(true);

                unloadCallback?.Invoke(Name);

                Clear();

                assetBundleLoadPool.Release(this);
            }
        }

        private void Clear()
        {
            name = null;
            assetBundle = null;
            assetPath = null;

            unloadCallback = null;
        }
    }
}