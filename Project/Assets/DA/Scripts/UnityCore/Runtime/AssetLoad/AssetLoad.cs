using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetLoad : IAssetLoad
    {
        private static System.Type typeAB = typeof(AssetBundle);

        private static ObjectPool.ObjectPool<AssetLoad> assetBundleLoadPool;

        private string name;
        public string Name { get { return name; } }

        private Object asset;
        public Object Asset { get { return asset; } }

        public AssetLoadState LoadState { get; private set; }

        private int referenceCount;

        private System.Action<Object> onLoaded;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private int loadedDirectDependentCount;
        private int totalDirectDependecntCount;

        private System.Type loadType;

        private System.Action<string> unloadCallback;


        private AssetLoader assetLoader;

        static AssetLoad()
        {
            assetBundleLoadPool = new ObjectPool.ObjectPool<AssetLoad>();
            assetBundleLoadPool.Initialize(() => new AssetLoad(), null, null, null);
        }

        public static AssetLoad GetAssetLoad(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            var assetBundleLoad = assetBundleLoadPool.Allocate();
            assetBundleLoad.Initialize(path, loadType, unloadCallback);
            return assetBundleLoad;
        }

        public void Initialize(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;

            this.unloadCallback = unloadCallback;

            assetLoader = AssetLoader.GetAssetLoader();
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    string abName = ABManifest.GetAssetBundleByAssetPath(assetPath);
                    var assetBundle = AssetBundle.LoadFromFile(abName);

                    if (loadType.Equals(typeAB) == false)
                    {
                        asset = assetBundle.LoadAsset(ABManifest.GetAssetNameByAssetPath(this.assetPath), loadType);
                    }

                    var directDependencies = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(abName));
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

                    var directDependencies = ABManifest.AssetBundleManifest.GetDirectDependencies(ABManifest.GetAssetNameByAssetPath(assetPath));
                    totalDirectDependecntCount = directDependencies.Length;
                    foreach (var directDependent in directDependencies)
                    {
                        assetLoader.LoadAssetBundleSync(directDependent, DirectDependentCallback);
                    }
                    this.onLoaded += onLoaded;

                    break;
                case AssetLoadState.Loading:
                case AssetLoadState.Loaded:

                    this.onLoaded += onLoaded;

                    break;
                case AssetLoadState.Unload:
                    Debug.LogError("资源已卸载，任在加载。");
                    break;
            }
        }

        private void DirectDependentCallback(Object loadAsstBundle)
        {
            loadedDirectDependentCount++;
            if (loadedDirectDependentCount == totalDirectDependecntCount)
            {
                assetBundleCreateRequest.completed += AssetBundleRequest_completed;
            }
        }
        private void AssetBundleRequest_completed(AsyncOperation asyncOperation)
        {
            LoadState = AssetLoadState.Loaded;

            var assetBundle = assetBundleCreateRequest.assetBundle;
            if (loadType.Equals(typeAB) == false)
            {
                asset = assetBundle.LoadAsset(System.IO.Path.GetFileName(this.assetPath), loadType);
            }
            assetBundleCreateRequest = null;

            onLoaded?.Invoke(asset);
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

                var assetBundle = asset as AssetBundle;
                if (assetBundle != null)
                    assetBundle.Unload(true);

                unloadCallback?.Invoke(Name);

                Clear();

                assetBundleLoadPool.Release(this);
            }
        }
        private void Clear()
        {

            unloadCallback = null;
        }
    }
}