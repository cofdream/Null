using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace DA.AssetsBundle
{
    public class AssetsBundleTool : EditorWindow
    {
        [MenuItem("DATool/AssetsBundle/Build Bundles")]
        private static void BuildBundles()
        {
            var watch = new Stopwatch();
            watch.Start();
            BuildScript.BuildRules();
            BuildScript.BuildAssetBundles();
            watch.Stop();
            Debug.Log("BuildBundles " + watch.ElapsedMilliseconds + " ms.");
        }
    }
}