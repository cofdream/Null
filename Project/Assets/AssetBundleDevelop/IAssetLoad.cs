using UnityEngine;

namespace NullNamespace
{
    public enum AssetLoadState
    {
        NotLoad = 0,
        Loading = 1,
        Loaded = 2,
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
        bool LoadAsset();
        void LoadAsset(System.Action<Object> p);
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

        //public event System.Action<bool, Object> OnLoadSu;
        //public event System.Action<bool, Object> OnLoad;

        private string assetPath;

        public AssetLoadState LoadState { get; private set; }

        public EditorLoad(string path)
        {
            referenceCount = 0;

            name = path;
            assetPath = path.Substring("resources://".Length);

            LoadState = AssetLoadState.NotLoad;
        }

        public bool LoadAsset()
        {
            Asset = /*UnityEditor.AssetDatabase.LoadAssetAtPath*/Resources.Load(this.assetPath);

            Debug.Log("New");

            return Asset == null;
        }

        public void LoadAsset(System.Action<Object> onLoaded)
        {
            resourceRequest = Resources.LoadAsync<GameObject>("");
            resourceRequest.completed += ResourceRequest_completed;

            OnLoaded += onLoaded;
        }

        private void ResourceRequest_completed(AsyncOperation obj)
        {
            Asset = resourceRequest.asset;

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
                if (Asset is GameObject)
                {
                    Asset = null;
                    //Resources.UnloadUnusedAssets();
                }
                else
                {
                    Resources.UnloadAsset(Asset);
                    Asset = null;
                }

                OnUnload?.Invoke(this);

                OnUnload = null;
            }
        }

    }
    public class AssetBundleLoad : IAssetLoad
    {
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

        private AssetLoader assetLoader;

        public AssetLoadState LoadState { get; private set; }

        public AssetBundleLoad(string path, AssetLoader assetLoader)
        {
            referenceCount = 0;
            assetPath = path;

            name = path;

            this.assetLoader = assetLoader;

            LoadState = AssetLoadState.NotLoad;
        }

        public bool LoadAsset()
        {
            if (LoadState == AssetLoadState.NotLoad)
            {

            }
            LoadState = AssetLoadState.NotLoad;

            assetBundle = AssetBundle.LoadFromFile(this.assetPath);

            directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));
            foreach (var directDependent in directDependentArray)
            {
                assetLoader.LoadAsset<AssetBundle>($"{AssetBundleConfig.AssetBundleRoot}/{directDependent}");
            }

            return Asset == null;
        }

        public void LoadAsset(System.Action<Object> onLoaded)
        {
            LoadState = AssetLoadState.NotLoad;

            assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(this.assetPath);

            directDependentArray = ABManifest.AssetBundleManifest.GetDirectDependencies(System.IO.Path.GetFileName(assetPath));

            assetBundleCreateRequest.completed += ResourceRequest_completed;

            OnLoaded += onLoaded;
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
                assetBundle.Unload(true);
                assetBundle = null;

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