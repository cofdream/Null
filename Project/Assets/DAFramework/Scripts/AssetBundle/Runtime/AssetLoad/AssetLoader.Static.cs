using DA.AssetsBundle;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DA
{
	public partial class AssetLoader
	{
#if UNITY_EDITOR
        public static bool IsSimulationMode = false;
#endif
		private static List<AssetLoader> loaders = new List<AssetLoader>();


        private static Manifest manifest;
        // asset - bundle  资源名称对应bundle名称的字典
        private static Dictionary<string, string> assetName_BundleNameDir = new Dictionary<string, string>();
        private static string assetBundleRootPath;


        public static void Init()
        {
#if UNITY_EDITOR
            if (AssetLoader.IsSimulationMode == false)
                assetBundleRootPath = AssetUtil.GetProjectPath($"AssetBundleFile/{AssetUtil.GetPlatform(Application.platform)}/");
            else
#endif
                assetBundleRootPath = AssetUtil.GetProjectPath("AssetBundleFile/");

            LoadMainfest();

            for (int i = 0; i < manifest.assets.Length; i++)
            {
                var ass = manifest.assets[i];
                assetName_BundleNameDir.Add(ass.name, manifest.bundles[ass.bundle].name);
            }
            Debug.Log(111);
        }
        private static void LoadMainfest()
        {
            string manifestPath = assetBundleRootPath + Manifest.AssetBundleName;

            AssetBundle assetBundle = AssetBundle.LoadFromFile(manifestPath);
            manifest = assetBundle.LoadAsset<Manifest>(Path.GetFileNameWithoutExtension(Manifest.AssetBundleName));
        }

        public static AssetLoader Loader
        {
            get
            {
                var loader = new AssetLoader();
                loaders.Add(loader);
                return loader;
            }
        }

        public static string GetAssetPath(string path)
        {
            if (assetName_BundleNameDir.TryGetValue(path, out string bundleName))
            {
                return assetBundleRootPath + bundleName;
            }
            return null;
        }
    }
}