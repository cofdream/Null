using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoadManager : MonoBehaviour
    {

        public static List<AssetLoad> AssetLoadList = new List<AssetLoad>();

        static AssetLoadManager()
        {
            var go = new GameObject("AssetLoadManager");
            go = Instantiate(go);
            go.AddComponent<AssetLoadManager>();
            DontDestroyOnLoad(go);
        }


        public static void AddIAssetLoad(AssetLoad assetLoad)
        {
            AssetLoadList.Add(assetLoad);
            assetLoad.OnUnload += RemoveIAssettLoad;
        }
        private static void RemoveIAssettLoad(AssetLoad assetLoad)
        {
            AssetLoadList.Remove(assetLoad);
            Debug.Log("RemoveIAssettLoad");
        }

    }

}