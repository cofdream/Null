using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1232321 : MonoBehaviour
{
    AssetBundle ab;
    AssetBundle ab2;
    GameObject go;
    GameObject go2;
    // Start is called before the first frame update
    void Start()
    {
        ab = AssetBundle.LoadFromFile(@"E:\Git\Null\Project\BuildAssetBundle\Windows\assets_cofdream_resource_prefabs");
        //ab2 = AssetBundle.LoadFromFile(@"E:\Git\Null\Project\BuildAssetBundle\Windows\assets_cofdream_resource_sprites");
        
        go = ab.LoadAsset<GameObject>("Cube");

        go2 = Instantiate<GameObject>(go);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        Destroy(go2);
        ab?.Unload(true);
        ab2?.Unload(true);
        AssetBundle.UnloadAllAssetBundles(true);
    }
}
