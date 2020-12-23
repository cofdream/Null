using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoadManager : MonoBehaviour
    {

        public static List<IAssetLoad> AssetLoadList = new List<IAssetLoad>();

        static AssetLoadManager()
        {
            var go = new GameObject("AssetLoadManager");
            go = Instantiate(go);
            go.AddComponent<AssetLoadManager>();
            DontDestroyOnLoad(go);
        }


        public static void AddIAssetLoad(IAssetLoad assetLoad)
        {
            AssetLoadList.Add(assetLoad);
            assetLoad.OnUnload += RemoveIAssettLoad;
        }
        private static void RemoveIAssettLoad(IAssetLoad assetLoad)
        {
            AssetLoadList.Remove(assetLoad);
            Debug.Log("RemoveIAssettLoad");
        }

    }

}