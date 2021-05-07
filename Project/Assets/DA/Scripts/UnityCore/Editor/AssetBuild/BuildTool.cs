using UnityEditor;

#if UNITY_EDITOR
namespace DA.AssetBuild
{
    public static class BuildTool
    {
        [MenuItem("DATools/AssetBuild/Build Rule")]
        public static void BuildRule()
        {
            DA.AssetBuild.BuildRule.GenerateBuildRule();
        }

        [MenuItem("DATools/AssetBuild/Build Asset Bundle")]
        public static void BuildAssetBundle()
        {
            DA.AssetBuild.BuildRule.GenerateAssetBundle();
        }
    }


    public class BuildRuleWindow : EditorWindow
    {

    }
} 
#endif