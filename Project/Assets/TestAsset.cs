using DA;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestAsset : MonoBehaviour
{
    DA.AssetLoader assetLoader;
    public string path = "Assets/Resources/Icons/img_icon_qq.png";
    public Image img;
    void Start()
    {
        assetLoader = DA.AssetLoader.Loader;

        // img.sprite = assetLoader.Load<Sprite>(path);//.LoadAsset<Sprite>("img_icon_qq");

        //var ab = assetLoader.Load<AssetBundle>(path);

        //img.sprite = ab.LoadAsset<Sprite>("img_icon_qqSSS").GetSprite("img_icon_qqSSS_1");

        //Sprite sprite;
        //img.sprite =

        // img.sprite = ab.LoadAllAssets<Sprite>()[0];

        //img.sprite = ab.LoadAllAssets<Sprite>()[1];

        //img.sprite = ab.LoadAllAssets<Sprite>()[2];

        //img.sprite = ab.LoadAllAssets<Sprite>()[3];

        //assetLoader.LoadAsync<AssetBundle>(path, (ss) =>
        //{
        //    var sprites = ss.LoadAllAssets<Sprite>();

        //    img.sprite = sprites[0];

        //    //img.sprite = ss;
        //    Debug.Log(111);
        //});//.LoadAsset<Sprite>("img_icon_qq");


        img.sprite = assetLoader.Load<AssetBundle>(Assets.GetAssetPath("img_icon_qq" + ".png")).LoadAsset<Sprite>("img_icon_qq");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            assetLoader.UnloadAll();
        }
    }

    private void OnDestroy()
    {
        assetLoader.UnloadAll();
        assetLoader.Dispose();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TestAsset))]
public class TestAssetExpand : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var testAsset = target as TestAsset;

        GUILayout.Label("资源 ：" + testAsset.path + "\n加载到内存的状态 ：" + AssetDatabase.IsMainAssetAtPathLoaded(testAsset.path).ToString());
    }
}
#endif