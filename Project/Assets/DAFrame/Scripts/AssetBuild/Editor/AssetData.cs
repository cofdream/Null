using System;
using UnityEngine;

namespace DA.AssetBuild
{
    [Serializable]
    public class AssetData
    {
        public string AssetBundleName;

        public string[] AssetNames;
    }

    [Serializable]
    public class BuildAssetData
    {
        public string AssetPath;
    }
}