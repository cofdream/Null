using System;
using UnityEngine;

namespace DA
{
    public class AssetBundleLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private string assetBundlePath;
        private ushort refNumber;
        private AssetLoadState loadState;

        public event Action<IAssetLoad> UnloadCallBack;

        private AssetLoader loader;

        private static string assetBundleRoot;
        private static AssetBundleManifest assetBundleManifest;

        static AssetBundleLoad()
        {
#if UNITY_EDITOR
            assetBundleRoot = $"{System.IO.Directory.GetParent(Application.dataPath).FullName}/AssetBundle/{AssetsBundle.AssetUtil.GetPlatform(Application.platform)}/";
#else
            assetBundleRoot = $"{Application.persistentDataPath}/AssetBundle/";
#endif

            var ab = AssetBundle.LoadFromFile(assetBundleRoot + AssetsBundle.AssetUtil.GetPlatform(Application.platform));
            assetBundleManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }


        public IAssetLoad Init(AssetLoader loader)
        {
            assetBundle = null;
            assetBundlePath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;

            UnloadCallBack = null;

            this.loader = loader;

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
        public T LoadAsset<T>(string assetPath) where T : UnityEngine.Object
        {
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetBundlePath = assetPath;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile(assetBundleRoot + assetBundlePath);

                var dependencies = assetBundleManifest.GetAllDependencies(assetBundle.name);
                foreach (var dependencie in dependencies)
                {
                    loader.Load<AssetBundle>(dependencie);
                }

                loadState = AssetLoadState.Loaded;
            }

            return Load() as T;
        }
        public void LoadAsync<T>(string assetPath, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:
                    assetBundlePath = assetPath;
                    loadState = AssetLoadState.Loading;

                    var createRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
                    createRequest.completed += (AsyncOperation asyncOperation) =>
                    {
                        loadState = AssetLoadState.Loaded;

                        assetBundle = createRequest.assetBundle;

                        loadCallBack?.Invoke(Load() as T);
                    };

                    var dependencies = assetBundleManifest.GetAllDependencies(System.IO.Path.GetFileNameWithoutExtension(assetBundlePath));
                    foreach (var dependencie in dependencies)
                    {
                        loader.LoadAsync<AssetBundle>(dependencie, null);
                    }

                    break;
                case AssetLoadState.Loaded:

                    loadCallBack?.Invoke(Load() as T);

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
                UnloadCallBack?.Invoke(this);
                assetBundle.Unload(true);

                this.loader = null;
            }
        }
    }
}