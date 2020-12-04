using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

namespace DA.AssetsBundle
{
    public enum PatchId
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    /// <summary>
    /// 资源打包的分组方式
    /// </summary>
    public enum GroupBy
    {
        None,
        Explicit,
        Filename,
        Directory,
    }

    [Serializable]
    public class AssetBuild
    {
        public string path;
        public PatchId patch;
        public string bundle;
        public GroupBy groupBy = GroupBy.Filename;


    }
    [Serializable]
    public class BundleBuild
    {
        public string assetBundleName;
        public string[] assetNames;
        public AssetBundleBuild ToAssetBundleBuild()
        {
            return new AssetBundleBuild()
            {
                assetBundleName = assetBundleName,
                assetNames = assetNames
            };
        }
    }

    /// <summary>
    /// 限制创建
    /// </summary>
    internal interface IBundleAsset
    {
    }

    public class BuildRules : ScriptableObject, IBundleAsset
    {
        internal const string AssetPath = "AssetBundle/Build_Rules.asset";


        private readonly Dictionary<string, string> _asset2Bundles = new Dictionary<string, string>();


        [Tooltip("构建的版本号")]
        public int version;
        [Tooltip("是否把资源名字哈希处理")]
        public bool nameByHash = true;
        [Tooltip("打包选项")]
        public BuildAssetBundleOptions buildBundleOptions = BuildAssetBundleOptions.ChunkBasedCompression;
        [Tooltip("BuildPlayer 的时候被打包的场景")]
        public SceneAsset[] scenesInBuild = new SceneAsset[0];
        [Tooltip("所有需要打包的资源")]
        public AssetBuild[] assets = new AssetBuild[0];
        [Tooltip("所有打包好的资源")]
        public BundleBuild[] bundles = new BundleBuild[0];


        internal AssetBundleBuild[] GetBuilds()
        {
            return Array.ConvertAll(bundles, input => input.ToAssetBundleBuild());
        }

        internal void Build()
        {
            Clear();
        }
        /// <summary>
        /// 校验Asset路径是否合法
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>

        private void Clear()
        {

        }
        private string GetBundle(AssetBuild assetBuild)
        {
            if (assetBuild.path.EndsWith(".shader"))
            {
                return RuledAssetBundleName("shaders");
            }
            switch (assetBuild.groupBy)
            {
                case GroupBy.Explicit: return RuledAssetBundleName(assetBuild.bundle);
                case GroupBy.Filename:
                    return RuledAssetBundleName(Path.Combine(Path.GetDirectoryName(assetBuild.path), Path.GetFileNameWithoutExtension(assetBuild.path)));
                case GroupBy.Directory: return RuledAssetBundleName(Path.GetDirectoryName(assetBuild.path));
                default: return string.Empty;
            }

        }
        private string RuledAssetBundleName(string assetName)
        {
            if (nameByHash)
            {
                return Utility.GetMD5Hash(assetName) + Assets.Extension;
            }

            return assetName.Replace("\\", "/").ToLower() + Assets.Extension;
        }

        private void CollectAssets()
        {
            int length = assets.Length;

            var assetBuilds = new List<AssetBuild>(length);

            for (int i = 0; i < length; i++)
            {
                AssetBuild assetBuild = assets[i];
                if (File.Exists(assetBuild.path) && ValidateAsset(assetBuild.path))
                {
                    assetBuilds.Add(assetBuild);
                }
            }

            foreach (var assetBuild in assetBuilds)
            {
                assetBuild.bundle = GetBundle(assetBuild);
                _asset2Bundles[assetBuild.path] = assetBuild.bundle;
            }
            assets = assetBuilds.ToArray();
        }

        internal static bool ValidateAsset(string assetPath)
        {
            if (!assetPath.StartsWith("Assets/"))
            {
                return false;
            }

            var ext = Path.GetExtension(assetPath).ToLower();
            return ext != ".dll" && ext != ".cs" && ext != ".meta" && ext != ".js" && ext != ".boo";
        }
    }
}