using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetLoadManager : MonoBehaviour
    {
#if UNITY_EDITOR
        public const string SIMULATION_MODE= "Asset bundle simulation mode";
        public static bool IsSimulationMode = UnityEditor.EditorPrefs.GetBool(SIMULATION_MODE, false);
#endif

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