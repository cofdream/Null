using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

namespace DA.AssetsBundle
{
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

    public class BuildRules : ScriptableObject
    {
        public const string AssetPath = "Assets/AssetBundle/Build_Rules.asset";

        private readonly List<string> duplicated = new List<string>();
        private readonly Dictionary<string, string[]> conflicted = new Dictionary<string, string[]>();
        private readonly Dictionary<string, HashSet<string>> tracker = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, string> asset2Bundles = new Dictionary<string, string>();


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


        public AssetBundleBuild[] GetBuilds()
        {
            return Array.ConvertAll(bundles, input => input.ToAssetBundleBuild());
        }

        public int AddVersion()
        {
            version = version + 1;
            return version;
        }
        private bool IsScene(string asset)
        {
            return asset.EndsWith(".unity");
        }
        private void Track(string asset, string bundle)
        {
            if (!asset2Bundles.ContainsKey(asset))
            {
                asset2Bundles[asset] = Path.GetFileNameWithoutExtension(bundle) + "_children" + Assets.Extension;
            }

            HashSet<string> hashSet;
            if (!tracker.TryGetValue(asset, out hashSet))
            {
                hashSet = new HashSet<string>();
                tracker.Add(asset, hashSet);
            }

            hashSet.Add(bundle);

            if (hashSet.Count > 1)
            {
                string bundleName;
                asset2Bundles.TryGetValue(asset, out bundleName);
                if (string.IsNullOrEmpty(bundleName))
                {
                    duplicated.Add(bundleName);
                }
            }
        }
        private Dictionary<string, List<string>> GetBundles()
        {
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var item in asset2Bundles)
            {
                var bundle = item.Value;
                List<string> list;
                if (!dictionary.TryGetValue(bundle, out list))
                {
                    list = new List<string>();
                    dictionary[bundle] = list;
                }

                if (!list.Contains(item.Key))
                {
                    list.Add(item.Key);
                }
            }
            return dictionary;
        }
        private void Clear()
        {
            duplicated.Clear();
            conflicted.Clear();
            tracker.Clear();
            asset2Bundles.Clear();
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
        public void Build()
        {
            Clear();
            CollectAssets();
            AnalysisAssets();
            OptimizeAssets();
            Save();

            EditorUtility.ClearProgressBar();

            EditorUtility.SetDirty(this);
            AssetDatabase.ImportAsset(AssetPath);
        }

        public void GroupAsset(string path, GroupBy groupBy = GroupBy.Filename)
        {
            bool Match(AssetBuild bundleAsset)
            {
                return bundleAsset.path.Equals(path);
            }
            var asset = ArrayUtility.Find(assets, Match);
            if (asset != null)
            {
                asset.groupBy = groupBy;
            }
            else
            {
                ArrayUtility.Add(ref assets, new AssetBuild()
                {
                    path = path,
                    groupBy = groupBy,
                });
            }
        }
        public void PatchAsset(string path, PatchId patch = 0)
        {
            bool Match(AssetBuild bundleAsset)
            {
                return bundleAsset.path.Equals(path);
            }
            var asset = ArrayUtility.Find(assets, Match);
            if (asset != null)
            {
                asset.patch = patch;
                return;
            }
            ArrayUtility.Add(ref assets, new AssetBuild()
            {
                path = path,
                patch = patch,
            });
        }
        public void RemoveGroupAsset(string path)
        {
            bool Match(AssetBuild bundleAsset)
            {
                return bundleAsset.path.Equals(path);
            }
            int index = ArrayUtility.FindIndex(assets, Match);
            if (index != -1)
            {
                ArrayUtility.RemoveAt(ref assets, index);
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
            var assetBuilds = new List<AssetBuild>(assets.Length);

            foreach (var assetBuild in assets)
            {
                if (ValidateAsset(assetBuild.path))
                {
                    assetBuilds.Add(assetBuild);
                }
            }

            foreach (var assetBuild in assetBuilds)
            {
                assetBuild.bundle = GetBundle(assetBuild);
                asset2Bundles[assetBuild.path] = assetBuild.bundle;
            }
            assets = assetBuilds.ToArray();
        }
        /// <summary>
        /// 验证Asset资源是否符合标准
        /// 1.必须在Assets下
        /// 2.不能是 dll cs meta js boo 文件
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns></returns>
        private bool ValidateAsset(string assetPath)
        {
            if (File.Exists(assetPath) && !assetPath.StartsWith("Assets/"))
            {
                return false;
            }

            var ext = Path.GetExtension(assetPath).ToLower();
            return ext != ".dll" && ext != ".cs" && ext != ".meta" && ext != ".js" && ext != ".boo";
        }
        /// <summary>
        /// 分析依赖
        /// </summary>
        private void AnalysisAssets()
        {
            var getBundles = GetBundles();
            float curProgress = 0, MaxProgress = getBundles.Count;
            foreach (var item in getBundles)
            {
                var bundle = item.Key;
                if (EditorUtility.DisplayCancelableProgressBar($"分析依赖{curProgress}/{MaxProgress}", bundle, curProgress / MaxProgress))
                {
                    break;
                }
                var assetPaths = getBundles[bundle];
                var assetPathArray = assetPaths.ToArray();
                if (assetPaths.Exists(IsScene) && !assetPaths.TrueForAll(IsScene))
                {
                    conflicted.Add(bundle, assetPathArray);
                }
                var dependencies = AssetDatabase.GetDependencies(assetPathArray, true);
                foreach (var asset in dependencies)
                {
                    if (ValidateAsset(asset))
                    {
                        Track(asset, bundle);
                    }
                }
                curProgress++;
            }
        }
        /// <summary>
        /// 优化资源
        /// </summary>
        private void OptimizeAssets()
        {
            {
                float curProgress = 0, maxProgress = conflicted.Count;
                foreach (var item in conflicted)
                {
                    if (EditorUtility.DisplayCancelableProgressBar($"优化冲突{curProgress}/{maxProgress}", item.Key, curProgress / maxProgress))
                    {
                        break;
                    }
                    var list = item.Value;
                    foreach (var asset in list)
                    {
                        if (!IsScene(asset))
                        {
                            duplicated.Add(asset);
                        }
                    }

                    curProgress++;
                }
            }
            {
                float curProgress = 0, maxProgress = conflicted.Count;
                foreach (var item in duplicated)
                {
                    if (EditorUtility.DisplayCancelableProgressBar($"优化冗余{curProgress}/{maxProgress}", item, curProgress / maxProgress))
                    {
                        break;
                    }
                    OptimizeAsset(item);
                }
            }
        }
        private void OptimizeAsset(string asset)
        {
            if (asset.EndsWith(".shader"))
            {
                asset2Bundles[asset] = RuledAssetBundleName("shaders");
            }
            else
            {
                asset2Bundles[asset] = RuledAssetBundleName(asset);
            }
        }

        private void Save()
        {
            var getbundles = GetBundles();

            bundles = new BundleBuild[getbundles.Count];
            int index = 0;
            foreach (var item in getbundles)
            {
                bundles[index] = new BundleBuild()
                {
                    assetBundleName = item.Key,
                    assetNames = item.Value.ToArray(),
                };
                index++;
            }
        }
    }
}