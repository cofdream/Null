using DA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    AssetLoader loader;
    AssetBundle ab;
    void Start()
    {
        loader = AssetLoader.Loader;

        loader.LoadAsync<AssetBundle>("cube", (obj) =>
        {
            ab = obj;
            var go = ab.LoadAsset<GameObject>("Cube");
            var i_go = Instantiate(go);
        });
    }

    private void OnDestroy()
    {
        loader.Unload(ab);
        loader.UnloadAll();
        loader = null;
    }
}