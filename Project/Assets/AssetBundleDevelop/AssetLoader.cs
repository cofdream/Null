using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoader
    {
        private List<IAssetLoad> allAssets = new List<IAssetLoad>();

        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                string path = "Assets/Resources/" + assetName + ".png";

                assetLoad = new EditorLoad(path);

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset();

            return assetLoad.Asset as T;
        }

        public void LoadAsset<T>(string assetName, System.Action<T> onLoaded) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                if (assetName.StartsWith("resources://"))
                {
                    assetLoad = new EditorLoad(assetName);
                }
                else
                {
                    assetLoad = new AssetBundleLoad(assetName, this);
                }

                allAssets.Add(assetLoad);
                AssetLoadManager.AddIAssetLoad(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset((asset) =>
            {
                onLoaded?.Invoke(asset as T);
            });

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

        private static IAssetLoad GetIAssetLoad(IEnumerable<IAssetLoad> collection, string assetName)
        {
            foreach (var item in collection)
                if (item.Name == assetName)
                    return item;
            return null;
        }


        public void UnloadAll()
        {
            foreach (var item in allAssets)
            {
                item.Release();
            }
            Debug.Log("UnloadAll");
            allAssets.Clear();
        }

    }
}