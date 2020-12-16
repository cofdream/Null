using System;
using UnityEngine;

namespace DA.AssetsBundle
{
    [Serializable]
    public class AssetRef
    {
        public string name;
        public int bundle;
        public int dir;
    }

    /// <summary>
    /// AB包的依赖
    /// </summary>
    [Serializable]
    public class BundleRef
    {
        public string name;
        public int id;
        public int[] children = new int[0];
        public long length;
        public string abHash;
        public string crc32Hash;
    }

    public class Manifest : ScriptableObject
    {
        public static readonly string AssetPath = "Assets/AssetBundle/Manifest.asset";
        public static readonly string AssetBundleName = "manifest.unity3d";

        public string[] activeVariants = new string[0];
        public string[] dirs = new string[0];
        public AssetRef[] assets = new AssetRef[0];
        public BundleRef[] bundles = new BundleRef[0];
    }
}