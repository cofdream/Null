using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoader
    {
        private List<AssetLoad> allAssets = new List<AssetLoad>();

       

        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            foreach (var item in allAssets)
            {
                if (item.Name == assetName)
                {
                    return item.Asset as T;
                }
            }

            foreach (var item in AssetLoadManager.AssetLoadList)
            {
                if (item.Name == assetName)
                {
                    item.Retatin();

                    allAssets.Add(item);

                    return item.Asset as T;
                }
            }

            //var asset = Resources.Load<T>(assetName);
            string path = "Assets/Resources/" + assetName + ".png"; 
            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

            AssetLoad assetLoad = new AssetLoad(asset);

            allAssets.Add(assetLoad);

            AssetLoadManager.AddIAssetLoad(assetLoad);

            assetLoad.Retatin();

            Debug.Log("New");

            return asset;
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