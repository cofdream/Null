using UnityEngine;
using UnityEditor;

namespace NullNamespace
{
    public class Test222
    {
        [MenuItem("LoadTest/Unload")]
        public static void Unload()
        {
            Resources.UnloadUnusedAssets();
        }

        [MenuItem("LoadTest/Load Check")]
        public static void Check()
        {
            string path = "Assets/Resources/Icons/img_icon_qq.png";
            string path2 = "Assets/Resources/Icons/img_icon_twitter.png";
            string path3 = "Assets/Resources/Icons/img_icon_wechat.png";
            Debug.Log("资源 ：" + path + "\n加载到内存的状态 ：" + AssetDatabase.IsMainAssetAtPathLoaded(path).ToString());
            Debug.Log("资源 ：" + path2 + "\n加载到内存的状态 ：" + AssetDatabase.IsMainAssetAtPathLoaded(path2).ToString());
            Debug.Log("资源 ：" + path3 + "\n加载到内存的状态 ：" + AssetDatabase.IsMainAssetAtPathLoaded(path3).ToString());
        }
    }
}