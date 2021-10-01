using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Cofdream.Core.Asset;
using System.IO;

namespace CofdreamEditor.Core.Asset
{
    public class AssetBundleBuildKit
    {
        [MenuItem("Asset Bundle BuildKit/Asset Bundle Build")]
        static void AssetBundleBuild()
        {
            string outputPath = Application.dataPath;
            outputPath = outputPath.Remove(outputPath.Length - 6);

            outputPath += @"BuildAssetBundle\Windows\";


            var buildRuleFolders = AssetDatabase.FindAssets(string.Empty, new string[] { "Assets/Cofdream/AssetBundleBuildRule" });

            List<IBuildRule> buildRules = new List<IBuildRule>(buildRuleFolders.Length);
            for (int i = 0; i < buildRuleFolders.Length; i++)
            {
                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(buildRuleFolders[i]));
                IBuildRule buildRule = obj as IBuildRule;
                if (buildRule == null)
                {
                    Debug.LogError($"对象不继承{typeof(IBuildRule)}请检查资源,{buildRuleFolders[i]}");
                    continue;
                }

                buildRules.Add(buildRule);
            }

            assetBundleBuilds = new List<AssetBundleBuild>(50);

            foreach (var bundleRule in buildRules)
            {
                bundleRule.CreateAssetBundleBuild(AssetBundleBuildCallBack);
            }

            var assetBundleManifest = BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuilds.ToArray(),
                BuildAssetBundleOptions.ChunkBasedCompression |
                BuildAssetBundleOptions.DeterministicAssetBundle |
                BuildAssetBundleOptions.StrictMode, EditorUserBuildSettings.activeBuildTarget);
        }

        private static List<AssetBundleBuild> assetBundleBuilds;
        private static void AssetBundleBuildCallBack(AssetBundleBuild assetBundleBuild)
        {
            assetBundleBuilds.Add(assetBundleBuild);
        }
    }
}