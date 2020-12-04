using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DA.AssetsBundle
{
    public static class BuildScript
    {
        public static string outputPath = "AssetBundleFile/" + GetPlatformName();

        private static string GetPlatformName()
        {
            return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
        }

        private static string GetPlatformForAssetBundles(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
#if UNITY_2017_3_OR_NEWER
                case BuildTarget.StandaloneOSX:
                    return "OSX";
#else
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
                    return "OSX";
#endif
                default:
                    return "Default";
            }
        }

        internal static void BuildRules()
        {
            BuildRules buildRules = GetOrCreateBuildRules();
            buildRules.Build();
        }

        internal static void BuildAssetBundles()
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            BuildRules rules = GetOrCreateBuildRules();
            AssetBundleBuild[] builds = rules.GetBuilds();
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, rules.buildBundleOptions, EditorUserBuildSettings.activeBuildTarget);

            if (manifest != null)
            {
                BuildManifest(manifest, outputPath, rules);
            }
        }

        private static void BuildManifest(AssetBundleManifest assetBundleManifest, string bundleDir, BuildRules rules)
        {
            Manifest manifest = GetOrCreateManifest();
            string[] allAssetBundleName = assetBundleManifest.GetAllAssetBundles();
            Dictionary<string, int> bundle_IndexDic = CreateBundle_IndexDictionary(allAssetBundleName);
            List<BundleRef> bundleRefs = GetBundleRefs(assetBundleManifest, bundleDir, allAssetBundleName, bundle_IndexDic); //保存ab的依赖
            var dirs = new List<string>();
            var assets = new List<AssetRef>();
            var patches = new List<VPatch>();

            int length = rules.assets.Length;

            for (int i = 0; i < length; i++)
            {
                AssetBuild item = rules.assets[i];
                var path = item.path;
                var dir = Path.GetDirectoryName(path).Replace("\\", "/");

                var index = dirs.FindIndex(dirArg => dirArg.Equals(dir));
                if (index == -1)
                {
                    //保存当前引用的下标
                    index = dirs.Count;
                    dirs.Add(dir);
                }
            }

        }

        private static List<BundleRef> GetBundleRefs(AssetBundleManifest assetBundleManifest, string bundleDir, string[] allAssetBundleName, Dictionary<string, int> bundle2Ids)
        {
            int length = allAssetBundleName.Length;
            var bundleRefs = new List<BundleRef>(length);

            for (int i = 0; i < length; i++)
            {
                string assetBundleName = allAssetBundleName[i];
                var dependecies = assetBundleManifest.GetAllDependencies(assetBundleName);
                var path = $"{bundleDir}/{assetBundleName}";

                if (File.Exists(path))
                {
                    using (var stream = File.OpenRead(path))
                    {
                        BundleRef bundleRef = new BundleRef()
                        {
                            name = assetBundleName,
                            id = i,
                            children = Array.ConvertAll(dependecies, assetBundleNameAge => bundle2Ids[assetBundleNameAge]),
                            length = stream.Length,
                            abHash = assetBundleManifest.GetAssetBundleHash(assetBundleName).ToString(),
                            crc32Hash = Utility.GetCRC32Hash(stream),
                        };

                        bundleRefs.Add(bundleRef);
                    }
                }
                else
                {
                    Debug.LogError(path + "file not exist.");
                }
            }
            return bundleRefs;
        }

        /// <summary>
        /// 创建一个 assetBundleName 和 对应下标映射的字典
        /// </summary>
        /// <param name="allAssetBundleName"></param>
        /// <returns></returns>
        private static Dictionary<string, int> CreateBundle_IndexDictionary(string[] allAssetBundleName)
        {
            var bundleToIndexDic = new Dictionary<string, int>();
            for (var index = 0; index < allAssetBundleName.Length; index++)
            {
                var bundle = allAssetBundleName[index];
                bundleToIndexDic[bundle] = index;
            }

            return bundleToIndexDic;
        }

        internal static Manifest GetOrCreateManifest()
        {
            return GetOrCreateAsset<Manifest>(AssetsBundle.Manifest.AssetPath);
        }
        internal static BuildRules GetOrCreateBuildRules()
        {
            return GetOrCreateAsset<BuildRules>(AssetsBundle.BuildRules.AssetPath);
        }
        private static T GetOrCreateAsset<T>(string path) where T : ScriptableObject, IBundleAsset
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
            }
            return asset;
        }
    }

}