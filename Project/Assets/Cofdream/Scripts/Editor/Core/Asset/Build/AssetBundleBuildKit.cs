using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Cofdream.Core.Asset;
using System.IO;

namespace CofdreamEditor.Core.Asset
{
    //IBuildRule 接口设计
    // 晚上整个流程

    public class AssetBundleBuildKit
    {
        [MenuItem("Asset Bundle BuildKit/Asset Bundle Build")]
        static void AssetBundleBuild()
        {
            string outputPath = Application.dataPath;
            outputPath = outputPath.Remove(outputPath.Length - 6);

            outputPath += @"BuildAssetBundle\Windows\";

            var assets = AssetDatabase.FindAssets(string.Empty, new string[] { "Assets/Cofdream/AssetBundleBuildRule" });
            List<IBuildRule> buildRules = new List<IBuildRule>(assets.Length);
            int buildCount = 0;
            for (int i = 0; i < assets.Length; i++)
            {
                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assets[i]);
                IBuildRule buildRule = obj as IBuildRule;
                if (buildRule == null)
                {
                    Debug.LogError($"对象不继承{typeof(IBuildRule)}请检查资源,{assets[i]}");
                    continue;
                }
                int count = buildRule.GetAssetBundleBuildCount();
                if (count == 0) continue;

                buildCount += count;
                buildRules.Add(buildRule);
            }

            startIndex = 0;
            assetBundleBuilds = new AssetBundleBuild[buildCount];

            foreach (var bundleRule in buildRules)
            {
                bundleRule.CreateAssetBundleBuild(AssetBundleBuildCallBack);
            }

            var assetBundleManifest = BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuilds,
                BuildAssetBundleOptions.ChunkBasedCompression |
                BuildAssetBundleOptions.DeterministicAssetBundle |
                BuildAssetBundleOptions.StrictMode, EditorUserBuildSettings.activeBuildTarget);
        }

        private static int startIndex;
        private static AssetBundleBuild[] assetBundleBuilds;
        private static void AssetBundleBuildCallBack(AssetBundleBuild assetBundleBuild)
        {
            assetBundleBuilds[startIndex] = assetBundleBuild;
        }
    }
}