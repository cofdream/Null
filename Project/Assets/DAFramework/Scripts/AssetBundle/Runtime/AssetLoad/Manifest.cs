using UnityEngine;

namespace DA
{
    public class Manifest
    {
        internal static string AssetBundleRoot;
        internal static AssetBundleManifest AssetBundleManifest;

        static Manifest()
        {

            // 加载 Manifest
#if UNITY_EDITOR
            AssetBundleRoot = $"{System.IO.Directory.GetParent(Application.dataPath).FullName}/AssetBundle/{AssetsBundle.AssetUtil.GetPlatform(Application.platform)}/";
#else
            assetBundleRoot = $"{Application.persistentDataPath}/AssetBundle/";
#endif

            var ab = AssetBundle.LoadFromFile(AssetBundleRoot + AssetsBundle.AssetUtil.GetPlatform(Application.platform));
            AssetBundleManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        }

    }
}