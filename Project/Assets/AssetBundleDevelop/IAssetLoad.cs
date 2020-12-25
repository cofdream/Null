using UnityEngine;

namespace NullNamespace
{
    public enum AssetLoadState
    {
        NotLoad = 0,
        Loading = 1,
        Loaded = 2,
        Unload = 3,
        LoadError = 4,
    }

    public interface IAssetLoad
    {
        AssetLoadState LoadState { get; }
        string Name { get; }

        Object Asset { get; }

        event System.Action<Object> OnLoaded;
        event System.Action<IAssetLoad> OnUnload;
        void Retain();
        void Release();
        void LoadAsset();
        void LoadAsset(System.Action<Object> p);
    }

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

                Asset = Resources.Load(this.assetPath);
                LoadState = AssetLoadState.Loaded;
            }
        }

        public void LoadAsset(System.Action<Object> onLoaded)
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

    public class EditorLoad : IAssetLoad
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

        public EditorLoad(string path, System.Type loadType)
        {
            referenceCount = 0;

            name = path;
            assetPath = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;
        }
        public void LoadAsset()
        {
            if (LoadState == AssetLoadState.NotLoad)
            {
                LoadState = AssetLoadState.Loading;

                Asset = UnityEditor.AssetDatabase.LoadAssetAtPath(this.assetPath, loadType);

                LoadState = AssetLoadState.Loaded;
            }
        }

        public void LoadAsset(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    string res = "Assets/Resources";
                    if (assetPath.StartsWith(res))
                    {
                        resourceRequest = Resources.LoadAsync(assetPath.Substring(res.Length), loadType);
                        resourceRequest.completed += ResourceRequest_completed;

                        OnLoaded += onLoaded;
                    }
                    else
                    {
                        LoadState = AssetLoadState.Loading;

                        new DA.Timer.TimerOnce(() =>
                        {
                            Asset = UnityEditor.AssetDatabase.LoadAssetAtPath(this.assetPath, loadType);

                            LoadState = AssetLoadState.Loaded;

                            LoadAsset();
                            OnLoaded?.Invoke(Asset);
                            OnLoaded = null;

                        }, 0.0001f);

                        OnLoaded += onLoaded;
                    }

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

                Asset = null;

                OnUnload?.Invoke(this);
                OnUnload = null;
            }
        }

    }

    public class AssetBundleLoad : IAssetLoad
    {
        private static System.Type typeAB = typeof(AssetBundle);

        private string name;
        public string Name { get { return name; } }

        private AssetBundle assetBundle;
        public Object Asset { get => assetBundle; }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<IAssetLoad> OnUnload;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private string[] directDependentArray;


        private System.Action<string> onLoadAssetAsync;
        private System.Action<string, System.Action> onLoadAssetSync;
        private System.Action<string> unloadAsset;

        public AssetLoadState LoadState { get; private set; }

        public AssetBundleLoad(string path, System.Action<string> onLoadAssetAsync, System.Action<string, System.Action> onLoadAssetSync, System.Action<string> unloadAsset)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            this.onLoadAssetAsync = onLoadAssetAsync;
            this.onLoadAssetSync = onLoadAssetSync;
            this.unloadAsset = unloadAsset;
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundle = AssetBundle.LoadFromFile(this.assetPath);

                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));
                    foreach (var directDependent in directDependentArray)
                    {
                        onLoadAssetAsync?.Invoke($"{AssetBundleConfig.AssetBundleRoot}/{directDependent}");
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

        public void LoadAsset(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(this.assetPath);

                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));

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

                assetBundle.Unload(true);
                assetBundle = null;

                OnUnload?.Invoke(this);
                OnUnload = null;
            }
        }
    }

    public class AssetLoad : IAssetLoad
    {
        private static System.Type typeAB = typeof(AssetBundle);

        private string name;
        public string Name { get { return name; } }

        private Object asset;
        public Object Asset { get { return asset; } }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<IAssetLoad> OnUnload;

        private AssetBundleCreateRequest assetBundleCreateRequest;

        private string assetPath;

        private int loadedDirectDependentCount;
        private string[] directDependentArray;


        private System.Action<string> loadAssetAsync;
        private System.Action<string, System.Action> loadAssetSync;
        private System.Action<string> unloadAsset;

        private System.Type loadType;


        public AssetLoadState LoadState { get; private set; }

        public AssetLoad(string path, System.Action<string> loadAssetAsync, System.Action<string, System.Action> loadAssetSync, System.Action<string> unloadAsset, System.Type loadType)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadAssetAsync = loadAssetAsync;
            this.loadAssetSync = loadAssetSync;
            this.unloadAsset = unloadAsset;

            this.loadType = loadType;
        }

        public void LoadAsset()
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    var assetBundle = AssetBundle.LoadFromFile(this.assetPath);

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

        public void LoadAsset(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    LoadState = AssetLoadState.Loading;

                    assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(this.assetPath);
                    directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));

                    foreach (var directDependent in directDependentArray)
                    {
                        loadAssetSync.Invoke(directDependent, DirectDependentCallback);
                    }
                    OnLoaded += onLoaded;

                    break;
                case AssetLoadState.Loading:
                case AssetLoadState.Loaded:

                case AssetLoadState.LoadError:

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
                    unloadAsset(item);
                }
                unloadAsset = null;

                var assetBundle = asset as AssetBundle;
                if (assetBundle != null)
                    assetBundle.Unload(true);

                asset = null;

                OnUnload?.Invoke(this);
                OnUnload = null;
            }
        }
    }

    public class ABManifest
    {
        private static AssetBundleManifest assetBundleManifest;

        public static AssetBundleManifest AssetBundleManifest
        {
            get
            {
                if (assetBundleManifest == null)
                {
                    AssetBundle assetBundle = AssetBundle.LoadFromFile(AssetBundleConfig.AssetBundleRoot);
                    assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }
                return assetBundleManifest;
            }
        }
    }

    public class AssetBundleConfig
    {

        public static string AssetBundleRoot = $"{Application.dataPath}/Window/";
    }

}