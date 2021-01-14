using System;
using UnityEngine;

#if UNITY_EDITOR
namespace DA.AssetBuild
{
    [Serializable]
    public class AssetData
    {
        public string AssetBundleName;

        public string[] AssetNames;

        public string[] AssetLoadPaths;
    }

    [Serializable]
    public class BuildAssetData
    {
        public string AssetPath;
    }
} 
#endif