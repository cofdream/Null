using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace DA.AssetBuild
{
    public sealed class BuildRule : ScriptableObject
    {
        public const string BuildRulePath = "Assets/DAAssetBundle/BundleRule.asset";

        public List<BuildAssetData> BuildAssetDatas;

        public List<AssetData> BuildAseet;

        public static void GenerateBuildRule()
        {
            var buildRule = GetBundleRule();

            buildRule.BuildAseet = new List<AssetData>();
            foreach (var buildAssetData in buildRule.BuildAssetDatas)
            {

                buildRule.BuildAseet.Add(new AssetData()
                {
                    AssetName = buildAssetData.AssetPath,
                    AssetBundleName = Path.GetFileNameWithoutExtension(buildAssetData.AssetPath),
                });
            }
        }

        public static BuildRule GetBundleRule()
        {
            var buildRule = AssetDatabase.LoadAssetAtPath<BuildRule>(BuildRulePath);
            if (buildRule == null)
            {
                buildRule = ScriptableObject.CreateInstance<BuildRule>();
                AssetDatabase.CreateAsset(buildRule, BuildRulePath);
                AssetDatabase.ImportAsset(BuildRulePath);
            }
            return buildRule;
        }
    }

}