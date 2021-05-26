using UnityEngine;

namespace DA.AssetLoad
{
    public enum AssetLoadState
    {
        NotLoad = 0,
        Loading = 1,
        Loaded = 2,
        Unload = 3,
        LoadError = 4,
    }

    public interface IAssetLoad
    {
        AssetLoadState LoadState { get; }
        string Name { get; }
        Object Asset { get; }

        void Retain();
        void Release();

        void LoadAsset();
        void LoadAssetSync(System.Action<Object> p);
    }

    public class ABManifest
    {
        private static AssetBundleManifest assetBundleManifest;

        public static AssetBundleManifest AssetBundleManifest
        {
            get
            {
                if (assetBundleManifest == null)
                {
                    AssetBundle assetBundle = AssetBundle.LoadFromFile(AssetBundleConfig.AssetBundleRoot + "Windows");
                    assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }
                return assetBundleManifest;
            }
        }

        // 基于资源的路径去获取ab的包名

        public static string GetAssetBundleByAssetPath(string assetPath)
        {
#if UNITY_EDITOR
            var buildRule = AssetBuild.BuildRule.GetBundleRule();

            foreach (var buildAsset in buildRule.BuildAseet)
            {
                foreach (var assetName in buildAsset.AssetNames)
                {
                    if (assetName == assetPath)
                    {
                        return AssetBundleConfig.AssetBundleRoot + buildAsset.AssetBundleName;
                    }
                }
            }

            return assetPath;
#else
            return null;
#endif
        }

        // 基于资源的路径去获取ab的资源名
        public static string GetAssetNameByAssetPath(string assetPath)
        {
            // todo 改成读配置
            return System.IO.Path.GetFileName(assetPath);
        }
    }

    public class AssetBundleConfig
    {
        public static string AssetBundleRoot = $"{System.IO.Directory.GetParent(Application.dataPath)}/BuildAssetBundle/Windows/";
    }

}