using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoader
    {
        public List<IAssetLoad> allAssets = new List<IAssetLoad>();

        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, typeof(T));

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset();

            return assetLoad.Asset as T; ;
        }
        public void LoadAssetSync<T>(string assetName, System.Action<T> onLoaded) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, typeof(T));

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAssetSync((asset) =>
            {
                onLoaded?.Invoke(asset as T);
            });

        }

        private void LoadAsset(string assetName)
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, null);

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset();
        }
        private void LoadAssetSync(string assetName, System.Action onLoaded)
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, null);

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAssetSync((asset) =>
            {
                onLoaded?.Invoke();
            });
        }


        private IAssetLoad CreateIAssetLoad(string assetName, System.Type loadType)
        {

#if UNITY_EDITOR
            if (assetName.StartsWith("resources://"))
            {
                return new ResourcesrLoad(assetName, loadType);
            }

            if (AssetLoadManager.IsSimulationMode)
            {
                return new EditorLoad(assetName, loadType);
            }
            else
#endif
            {
                if (assetName.EndsWith(".ab"))
                {
                    return new AssetBundleLoad(assetName, LoadAsset, LoadAssetSync, Unload);
                }
                else
                {
                    return new AssetLoad(assetName, LoadAsset, LoadAssetSync, Unload, loadType);
                }
            }

            return null;
        }

        private IAssetLoad GetIAssetLoad(string assetName)
        {
            IAssetLoad assetLoad = GetIAssetLoad(allAssets, assetName);
            if (assetLoad != null)
                return assetLoad;


            assetLoad = GetIAssetLoad(AssetLoadManager.AssetLoadList, assetName);
            if (assetLoad != null)
            {
                assetLoad.Retain();
                allAssets.Add(assetLoad);

                return assetLoad;
            }

            return null;
        }

        public void Unload(string assetName)
        {
            foreach (var item in allAssets)
                if (item.Name == assetName)
                {
                    item.Release();
                    return;
                }
            Debug.Log("删除失败," + assetName);
            return;
        }

        public void UnloadAll()
        {
            foreach (var item in allAssets)
            {
                item.Release();
            }
            allAssets.Clear();
        }

        private static IAssetLoad GetIAssetLoad(IEnumerable<IAssetLoad> collection, string assetName)
        {
            foreach (var item in collection)
                if (item.Name == assetName)
                    return item;
            return null;
        }
    }
}