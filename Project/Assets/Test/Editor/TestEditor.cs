using UnityEngine;
using UnityEditor;
using System.IO;
using DA;
using DATools;

namespace NullNamespace
{
    public class TestEditor : EditorWindow
    {
        [MenuItem("AssetBundle/Window", false, 0)]
        static void Open()
        {
            var wind = EditorMainWindow.GetWindowInCenter<TestEditor>();
            wind.Show();
        }


        static AssetBundle ab;
        private void OnGUI()
        {
            if (GUILayout.Button("Build AB"))
            {
                string output = Directory.GetParent(Application.dataPath).FullName + "/AssetBundle/" + DA.AssetsBundle.AssetUtil.GetPlatform(Application.platform);

                if (Directory.Exists(output) == false)
                {
                    Directory.CreateDirectory(output);
                }

                UnityEditor.BuildPipeline.BuildAssetBundles(output, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
            }


            GUILayout.Space(1);
            GUILayout.Space(1);

            if (GUILayout.Button("Unload cube"))
            {

                AssetBundle.UnloadAllAssetBundles(true);
                EditorUtility.UnloadUnusedAssetsImmediate();
                Debug.Log("UnloadAB");
            }
        }
    }
}