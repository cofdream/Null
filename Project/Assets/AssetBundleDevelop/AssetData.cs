using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public class AssetData
    {
        public string Name;
        public string AssetBundleName;
    }

    public class AssetBundleData
    {
        public string Name;
        public List<AssetData> AssetDatas = new List<AssetData>();
        public string[] DependenciesBundleName;
    }

    public class ResData
    {
        public List<AssetBundleData> assetBundleDatas = new List<AssetBundleData>();

        private void rrr()
        {
            //UnityEditor.AssetDatabase.GetAllAssetBundleNames
        }

    }


}