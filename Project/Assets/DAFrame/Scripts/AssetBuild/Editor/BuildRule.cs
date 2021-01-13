using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace DA.AssetBuild
{
    public sealed class BuildRule : ScriptableObject
    {
        public const string BuildRulePath = "Assets/DAAssetBundle/BundleRule.asset";

        public BuildAssetData[] BuildAssetDatas;

        public List<AssetData> BuildAseet;


        public void Run()
        {
            BuildAseet = new List<AssetData>();

            foreach (var buildAssetData in BuildAssetDatas)
            {
                // 判断该路径是文件还是文件夹，如果是文件夹
                if (File.GetAttributes(buildAssetData.AssetPath).CompareTo(FileAttributes.Directory) == 0)
                {
                    string[] files = Directory.GetFiles(buildAssetData.AssetPath);

                    List<string> files_Asset = new List<string>();
                    foreach (var file in files)
                    {
                        string extension = Path.GetExtension(file);
                        if (extension != ".meta" && extension != ".cs" && extension != ".dll")
                        {
                            files_Asset.Add(file);
                        }
                    }
                    if (files_Asset.Count > 0)
                    {
                        var assetData = new AssetData();

                        assetData.AssetNames = files_Asset.ToArray();


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

                        BuildAseet.Add(assetData);
                    }
                }
                else
                {
                    string file = buildAssetData.AssetPath;

                    string extension = Path.GetExtension(file);

                    if (extension != ".meta" && extension != ".cs" && extension != ".dll")
                    {
                        var assetData = new AssetData();

                        assetData.AssetNames = new string[] { file };


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

                        BuildAseet.Add(assetData);
                    }
                }
            }
        }

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

        public static void GenerateBuildRule()
        {
            var buildRule = GetBundleRule();

            buildRule.Run();

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