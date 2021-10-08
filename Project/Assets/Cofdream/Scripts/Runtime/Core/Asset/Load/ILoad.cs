using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cofdream.Core.Asset
{
    public interface ILoad
    {

    }

    public class AssetBundleLoad : ILoad
    {
        public void Load(string name)
        {
            string outputPath = Application.dataPath;
            outputPath = outputPath.Remove(outputPath.Length - 6);
            outputPath += @"BuildAssetBundle\Windows\";

            var ab = AssetBundle.LoadFromFile(outputPath + name);

            AssetBundleManifestArray assetBundleManifestArray = JsonUtility.FromJson<AssetBundleManifestArray>(outputPath + "/AssetBundleManifest.json");
            
            // q
            //ab - asset []
        }

    }
    public class AssetLoad : ILoad
    {
        public void Load(string path, string name)
        {
            // "Assets/Cofdream/Resource/BattleMap/10001/Prefabs/Map_10001.prefab"

            AssetBundle assetBundle = null;
            //assetBundle.LoadAsset();
        }
        public void Load()
        {

        }
    }
}
