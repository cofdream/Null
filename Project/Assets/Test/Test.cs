using DA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    AssetLoader loader;
    GameObject ab;
    void Start()
    {
        loader = AssetLoader.Loader;

        // loader.Load<AssetBundle>("ma3");

        loader.LoadAsync<GameObject>("Cube3","", (obj) =>
        {
            ab = obj;
            var i_go = Instantiate(obj);
        });
    }

    private void OnDestroy()
    {
        loader.Unload(ab);
        loader = null;
    }
}
