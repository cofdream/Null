using DA.AssetLoad;
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

        [MenuItem("DATools/AssetBuild/SimulationMode", true)]
        public static bool CheckSimulationMode()
        {
            Menu.SetChecked("DATools/AssetBuild/SimulationMode", EditorPrefs.GetBool(AssetLoadManager.SIMULATION_MODE, false));
            return true;
        }

        [MenuItem("DATools/AssetBuild/SimulationMode")]
        public static void SimulationMode()
        {
            bool simulationMode = !EditorPrefs.GetBool(AssetLoadManager.SIMULATION_MODE, false);
            EditorPrefs.SetBool(AssetLoadManager.SIMULATION_MODE, simulationMode);

            EditorUtility.DisplayDialog("Tip", simulationMode ? "Change load mode: Simulation Mode" : "Change load mode: Local Mode", "OK");
        }
    }


    public class BuildRuleWindow : EditorWindow
    {

    }
}
#endif