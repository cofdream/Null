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

        img.sprite = assetLoader.Load<Sprite>(path);
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

