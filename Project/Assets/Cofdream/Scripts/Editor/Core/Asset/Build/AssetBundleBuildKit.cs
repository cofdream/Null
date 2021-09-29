using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Cofdream.Core.Asset;
using System.IO;
using UnityEngine.UI;
using DATools;

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

            var assetBundles = AssetDatabase.LoadAssetAtPath<AssetBundles>("Assets/Cofdream/Scripts/Core/Asset/AssetBundles.asset");

            List<AssetBundleBuild> buildList = new List<AssetBundleBuild>(assetBundles.Builds.Length);

            foreach (var bundle in assetBundles.Builds)
            {

                if (File.Exists(bundle.Path))
                {
                    buildList.Add(new AssetBundleBuild()
                    {
                        assetBundleName = bundle.Name,
                        assetNames = new string[] { bundle.Path },
                    });
                }
                else
                {
                    string[] paths = AssetDatabase.FindAssets("", new string[] { bundle.Path });
                    for (int i = 0; i < paths.Length; i++)
                    {
                        paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);

                        var ob = AssetDatabase.LoadAssetAtPath<Object>(paths[i]);
                        Debug.Log(ob);
                        Debug.Log(paths[i]);
                    }

                    buildList.Add(new AssetBundleBuild()
                    {
                        assetBundleName = bundle.Name,
                        assetNames = paths,

                    });
                }
            }

            var assetBundleManifest = BuildPipeline.BuildAssetBundles(outputPath, buildList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}