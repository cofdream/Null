using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BuildAssetBundle_Test
    {
        [Test]
        public void BuildAssetBundle()
        {
            string output = Directory.GetParent(Application.dataPath).FullName + "/TestingBuild_AB/Windows";

            if (Directory.Exists(output) == false)
            {
                Directory.CreateDirectory(output);
            }

            AssetBundleBuild[] builds = new AssetBundleBuild[]
            {
                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Prefabs_Cube.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Prefabs/Cube.prefab"),
                },
                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Prefabs_Images.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Prefabs/Images.prefab"),
                },


                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Materials_CubeMat.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Materials/CubeMat.mat"),
                },


                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Icons_img_icon_qq.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Icons/img_icon_qq.png"),
                },
                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Icons_img_icon_twitter.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Icons/img_icon_twitter.png"),
                },
                new AssetBundleBuild()
                {
                    assetBundleName = "Tests_BuildAssetBundle_Icons_img_icon_wechat.ab".ToLower(),
                    assetNames = GetDependencies("Assets/Tests_BuildAssetBundle/Icons/img_icon_wechat.png"),
                },

            };

            BuildPipeline.BuildAssetBundles(output, builds, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        }

        private static string[] GetDependencies(string path)
        {
            //string[] dependencies = AssetDatabase.GetDependencies(path);
            //List<string> array = new List<string>(dependencies.Length);
            //foreach (var dependent in dependencies)
            //{
            //    string extension = Path.GetExtension(dependent).ToLower();
            //    if (extension != ".dll" && extension != ".cs" && extension != ".meta" && extension != ".js" && extension != ".boo")
            //    {
            //        array.Add(dependent);
            //    }
            //}

            //return array.ToArray();

            return new string[] { path };
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BuildAssetBundle_TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
