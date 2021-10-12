using Cofdream.Core.Asset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLoad : MonoBehaviour
{
    public string[] Paths;

    public Image image;

    private AssetsLoad assetsLoad;

    public List<Object> allAssets;

    void Start()
    {
        allAssets = new List<Object>();

        assetsLoad = new AssetsLoad("assets_cofdream_resource_battlemap_10001");
        allAssets.Add(assetsLoad.Load("Map_10001.prefab", typeof(GameObject)) as GameObject);

        //loader.Load<GameObject>("assets_cofdream_resource_battlemap_10001", "Map_10003.prefab");
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        assetsLoad.UnLoad();
    }
}
