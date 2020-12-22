using System;
using System.Collections.Generic;
using UnityEngine;

namespace DA
{
    public class AssetBundleLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private string assetBundlePath;
        private ushort refNumber;
        private AssetLoadState loadState;

        private Action<UnityEngine.Object> loadedCallback;
        private AssetBundleCreateRequest createRequest;

        public event Action<IAssetLoad> UnloadCallback;

        private AssetLoader loader;

        private int dependentIndex;
        private int dependentCount;
        private AssetBundle[] assetBundleDependencies;

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


        public AssetBundleLoad Init(AssetLoader loader)
        {
            assetBundle = null;
            assetBundlePath = null;
            refNumber = 0;
            loadState = AssetLoadState.NotLoaded;

            loadedCallback = null;
            createRequest = null;
            UnloadCallback = null;

            dependentCount = 0;
            assetBundleDependencies = null;

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
        public UnityEngine.Object LoadAsset(string assetPath)
        {
            if (loadState == AssetLoadState.Loading)
            {
                Debug.LogError("处于异步加载中");
            }
            if (loadState == AssetLoadState.NotLoaded)
            {
                assetBundlePath = assetPath;
                loadState = AssetLoadState.Loading;

                assetBundle = AssetBundle.LoadFromFile(assetBundleRoot + assetBundlePath);

                var dependencies = assetBundleManifest.GetAllDependencies(assetBundle.name);
                dependentCount = dependencies.Length;
                if (dependentCount != 0)
                {
                    assetBundleDependencies = new AssetBundle[dependentCount];
                    dependentIndex = 0;
                    foreach (var dependencie in dependencies)
                    {
                        //var asseBundleDependent = loader.Load<AssetBundle>(dependencie);

                       // assetBundleDependencies[dependentIndex] = asseBundleDependent;
                        dependentIndex++;
                    }
                }

                loadState = AssetLoadState.Loaded;
            }

            return Load();
        }
        public void LoadAsync(string assetPath, Action<UnityEngine.Object> callback)
        {
            switch (loadState)
            {
                case AssetLoadState.NotLoaded:
                    assetBundlePath = assetPath;
                    loadState = AssetLoadState.Loading;
                    loadedCallback += callback;

                    createRequest = AssetBundle.LoadFromFileAsync(assetBundleRoot + assetBundlePath);

                    var dependencies = assetBundleManifest.GetAllDependencies(System.IO.Path.GetFileNameWithoutExtension(assetBundlePath));
                    dependentCount = dependencies.Length;
                    if (dependentCount != 0)
                    {
                        assetBundleDependencies = new AssetBundle[dependencies.Length];
                        foreach (var dependent in dependencies)
                        {
                            //loader.LoadAsync<AssetBundle>(dependent, DependentLoadCallBack);
                        }
                    }
                    else
                    {
                        createRequest.completed += AssetBundleLoadedCallBack;
                    }

                    break;
                case AssetLoadState.Loading:
                    loadedCallback += callback;
                    break;

                case AssetLoadState.Loaded:
                    loadedCallback?.Invoke(Load());
                    break;
            }
        }

        private UnityEngine.Object Load()
        {
            refNumber++;
            Debug.Log($"Load {assetBundlePath} Done.");
            return assetBundle;
        }

        private void AssetBundleLoadedCallBack(AsyncOperation asyncOperation)
        {
            loadState = AssetLoadState.Loaded;

            assetBundle = createRequest.assetBundle;
            createRequest = null;

            loadedCallback?.Invoke(Load());
        }

        private void DependentLoadCallBack(AssetBundle assetBundle)
        {
            assetBundleDependencies[dependentIndex] = assetBundle;
            dependentIndex++;

            if (dependentIndex == dependentCount)
            {
                createRequest.completed += AssetBundleLoadedCallBack;
            }
        }

        public void Unload()
        {
            refNumber--;

            if (refNumber == 0)
            {
                loadState = AssetLoadState.Unload;
                UnloadCallback?.Invoke(this);

                //卸载依赖
                if (dependentCount != 0)
                {
                    foreach (var assetBundleDependent in assetBundleDependencies)
                    {
                        loader.Unload(assetBundleDependent);
                    }
                    assetBundleDependencies = null;
                }

                assetBundle.Unload(true);
                Debug.Log($"Unload {assetBundlePath} Done.");

                this.loader = null;
            }
        }
    }
}