using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
namespace DA.AssetBuild
{
    public sealed class BuildRule : ScriptableObject
    {
        public const string BuildRulePath = "Assets/DAAssetBundle/BundleRule.asset";

        public BuildAssetData[] BuildAssetDatas;

        public List<AssetData> BuildAseet;


        public static void GenerateAssetBundle()
        {
            string output = Directory.GetParent(Application.dataPath).FullName + "/BuildAssetBundle/Windows";
            if (Directory.Exists(output) == false)
            {
                Directory.CreateDirectory(output);
            }

            var assetDatas = GetBundleRule().BuildAseet;
            List<AssetBundleBuild> buildsList = new List<AssetBundleBuild>(assetDatas.Count);
            foreach (var assetData in assetDatas)
            {
                buildsList.Add(new AssetBundleBuild()
                {
                    assetBundleName = assetData.AssetBundleName,
                    assetNames = assetData.AssetNames,
                });
            }

            BuildPipeline.BuildAssetBundles(output, buildsList.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        }
        // todo  asset Name 改为全目录
        public static void GenerateBuildRule()
        {
            var buildRule = GetBundleRule();

            buildRule.BuildAseet = new List<AssetData>();

            foreach (var buildAssetData in buildRule.BuildAssetDatas)
            {
                // 判断该路径是文件还是文件夹
                if (File.GetAttributes(buildAssetData.AssetPath).CompareTo(FileAttributes.Directory) == 0)
                {
                    //是文件夹
                    string[] files = Directory.GetFiles(buildAssetData.AssetPath);

                    List<string> files_Asset = new List<string>();
                    List<string> assetLoadPaths = new List<string>();
                    foreach (var file in files)
                    {
                        string extension = Path.GetExtension(file);
                        if (extension != ".meta" && extension != ".cs" && extension != ".dll")
                        {
                            files_Asset.Add(file.Replace("\\", @"/"));

                            string loadPath;
                            if (file.StartsWith("Assets/"))
                            {
                                loadPath = file.Replace("\\", @"/").Substring("Assets/".Length);
                            }
                            else
                            {
                                loadPath = file.Replace("\\", @"/");
                            }
                            loadPath = loadPath.Substring(0, loadPath.LastIndexOf("."));
                            assetLoadPaths.Add(loadPath);
                        }
                    }
                    if (files_Asset.Count > 0)
                    {
                        var assetData = new AssetData();

                        assetData.AssetNames = files_Asset.ToArray();
                        assetData.AssetLoadPaths = assetLoadPaths.ToArray();


                        string tempPath = Directory.GetParent(buildAssetData.AssetPath).Name;
                        if (tempPath != null)
                        {
                            string tempPath2 = Directory.GetParent(tempPath).Name;
                            if (tempPath2 != null)
                            {
                                assetData.AssetBundleName = (tempPath2 + "_" + tempPath + "_" + Path.GetFileNameWithoutExtension(buildAssetData.AssetPath) + ".ab").ToLower();
                            }
                            else
                            {
                                assetData.AssetBundleName = (tempPath + "_" + Path.GetFileNameWithoutExtension(buildAssetData.AssetPath) + ".ab").ToLower();
                            }
                        }

                        buildRule.BuildAseet.Add(assetData);
                    }
                }
                else
                {
                    string file = buildAssetData.AssetPath;

                    string assetLoadPath;
                    if (file.StartsWith("Assets/"))
                    {
                        assetLoadPath = file.Substring("Assets/".Length);
                    }
                    else
                    {
                        assetLoadPath = file;
                    }
                    assetLoadPath = assetLoadPath.Substring(0, assetLoadPath.LastIndexOf("."));

                    string extension = Path.GetExtension(file);

                    if (extension != ".meta" && extension != ".cs" && extension != ".dll")
                    {
                        var assetData = new AssetData();

                        assetData.AssetNames = new string[] { file };
                        assetData.AssetLoadPaths = new string[] { assetLoadPath };

                        string tempPath = Directory.GetParent(file).Name;
                        if (tempPath != null)
                        {
                            string tempPath2 = Directory.GetParent(tempPath).Name;
                            if (tempPath2 != null)
                            {
                                assetData.AssetBundleName = (tempPath2 + "_" + tempPath + "_" + Path.GetFileNameWithoutExtension(file) + ".ab").ToLower();
                            }
                            else
                            {
                                assetData.AssetBundleName = (tempPath + "_" + Path.GetFileNameWithoutExtension(file) + ".ab").ToLower();
                            }
                        }

                        buildRule.BuildAseet.Add(assetData);
                    }
                }
            }

            EditorUtility.SetDirty(buildRule);
            AssetDatabase.ImportAsset(BuildRulePath);
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
#endif