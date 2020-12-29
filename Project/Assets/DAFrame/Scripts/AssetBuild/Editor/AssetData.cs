using System;
using UnityEngine;

namespace DA.AssetBuild
{
    [Serializable]
    public class AssetData
    {
        public string AssetName;
        public string AssetBundleName;
    }

    [Serializable]
    public class BuildAssetData
    {
        public string AssetPath;
    }
}