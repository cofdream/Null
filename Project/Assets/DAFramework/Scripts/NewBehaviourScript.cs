using DA.AssetsBundle;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.CanvasScaler;
using Object = UnityEngine.Object;

public class NewBehaviourScript : MonoBehaviour
{
    UnityEngine.Object AA;


    //IEnumerator Start()
    //{

    //    BuildPipeline.BuildAssetBundles(@"E:\Git\Null\Project\AssetBundleFile\", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

    //    string dlcPath = Application.persistentDataPath + "/DLC/";


    //    var rules = BuildScript.GetOrCreateBuildRules();
    //    //Assets/Resources/Prefabs/UI/LoginWIndow/LoginWIndow.prefab
    //    //Assets/Scenes/SplashScreen.unity
    //    var assetBuild = Array.Find(rules.assets, (asset) => asset.path == "Assets/Scenes/SplashScreen.unity");

    //    //var assetBundle = AssetBundle.LoadFromFileAsync(@"E:\Git\Null\Project\AssetBundleFile\Windows/" + assetBuild.bundle);
    //    //yield return assetBundle;

    //    //var bundle = assetBundle.assetBundle;

    //    AA = AssetDatabase.LoadAssetAtPath(assetBuild.path, typeof(Scene));

    //    //SceneManager.LoadScene("SplashScreen");

    //    //SceneManager.LoadScene(,"SplashScreen.unity", LoadSceneMode.Additive);


    //    //var go = assetBundle.LoadAsset("SplashScreen.unity");

    //    //Instantiate(go);

    //    yield return null;
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildPipeline.BuildAssetBundles(@"E:\Git\Null\Project\AssetBundleFile\", new AssetBundleBuild[] {

             new AssetBundleBuild(){
             assetBundleName = "Assets/Scenes/SplashScreen.unity",
             assetBundleVariant = "",
             }

             }, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            string path = "Assets/Scenes/SplashScreen.unity";
            string guiid = AssetDatabase.AssetPathToGUID(path);

            var lastScene = EditorBuildSettings.scenes;
            int length = lastScene.Length;

            string ebsPath = @"E:\Git\Null\Project\ProjectSettings\EditorBuildSettings.asset";
            string[] ebs = File.ReadAllLines(ebsPath);

            string head = "  m_Scenes:";
            List<string> ebsData = new List<string>(ebsPath.Length + 1);
            for (int i = 0; i < ebs.Length; i++)
            {
                if (ebs[i].StartsWith(head))
                {
                    ebsData.Add(head);
                    ebsData.Add($" - enabled: {1}");
                    ebsData.Add($"   path: {path}");
                    ebsData.Add($"   guid: {guiid}");
                }
                else
                {
                    ebsData.Add(ebs[i]);
                }
            }

            File.WriteAllLines(ebsPath, ebsData);

            EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[length + 1];
            Array.Copy(scenes, lastScene, length);
            var t = new EditorBuildSettingsScene()
            {
                enabled = true,
                path = path,
                guid = new GUID(guiid),
            };

            scenes[lastScene.Length] = t;

            EditorBuildSettings.scenes = scenes;



        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/SplashScreen.unity");

            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            //SceneManager.LoadScene("SplashScreen");
            ////SceneManager.LoadScene(0);

            //EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/SplashScreen.unity",new LoadSceneParameters());
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            //SceneManager.LoadScene("SplashScreen");
            ////SceneManager.LoadScene(0);

            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/SplashScreen.unity", default);
        }
    }
}
