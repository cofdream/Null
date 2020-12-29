using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DA.AssetBuild
{
    public static class BuildTool
    {
        [MenuItem("DATools/AssetBuild/Build Rule")]
        public static void RUn_BuildRule()
        {
            BuildRule.GenerateBuildRule();
        }
    }


    public sealed class BuildRule : ScriptableObject
    {
        static string BuildRulePath = Application.dataPath + "DAAssetBundle/BundleRule.asset";



        public static void GenerateBuildRule()
        {
            var buildRule = GetBundleRule();
            //buildRule.
        }

        public static BuildRule GetBundleRule()
        {
            var buildRule = AssetDatabase.LoadAssetAtPath<BuildRule>(BuildRulePath);
            if (buildRule == null)
            {
                buildRule = CreateInstance<BuildRule>();
                AssetDatabase.CreateAsset(buildRule, BuildRulePath);
                AssetDatabase.ImportAsset(BuildRulePath);
            }
            return buildRule;
        }
    }
    public class BuildRuleWindow : EditorWindow
    {

    }




    public class AssetData
    {
        public string Name;
        public string BundleName;
    }

    public class AssetBundleData
    {
        public string Name;
        public List<AssetData> AssetDatas = new List<AssetData>();
        public string[] DependencyBundleNames;
    }
}