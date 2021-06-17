using DA.AssetLoad;
using NullNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string Path;
    public MyFSM MyFSM;

    public AssetLoader loader;
    void Start()
    {
        //loader = AssetLoader.GetAssetLoader();

        //var all = FindObjectsOfType<MyFSM>(true);

        //foreach (var item in all)
        //{
        //    Debug.Log(item.name);
        //}
    }

    private void OnDestroy()
    {
        //loader.Unload(Path);
    }
}
