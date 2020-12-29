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


    public class BuildRuleWindow : EditorWindow
    {

    }
}