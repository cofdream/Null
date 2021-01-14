using UnityEngine;

#if UNITY_EDITOR
namespace DA.AssetLoad
{
    public class EditorLoad : IAssetLoad
    {
        private string name;
        public string Name { get { return name; } }

        public Object Asset { get; private set; }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<string> OnUnload;

        private string assetPath;

        public AssetLoadState LoadState { get; private set; }

        private System.Type loadType;

        public EditorLoad(string path, System.Type loadType)
        {
            referenceCount = 0;

            name = path;
            assetPath = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;
        }
        public void LoadAsset()
        {
            if (LoadState == AssetLoadState.NotLoad)
            {
                LoadState = AssetLoadState.Loading;

                Asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetPathConvertEditoPath(this.assetPath, loadType), loadType);

                LoadState = AssetLoadState.Loaded;
            }
        }

        public void LoadAssetSync(System.Action<Object> onLoaded)
        {
            LoadAsset();
            onLoaded?.Invoke(Asset);
        }

        public void Retain()
        {
            referenceCount++;
        }
        public void Release()
        {
            referenceCount--;

            if (referenceCount == 0)
            {
                LoadState = AssetLoadState.Unload;

                if (Asset is GameObject == false)
                    Resources.UnloadAsset(Asset);

                Asset = null;

                OnUnload(name);
                OnUnload = null;
            }
        }


        private static string AssetPathConvertEditoPath(string path, System.Type assetType)
        {
            var buildRule = AssetBuild.BuildRule.GetBundleRule();

            foreach (var buildAsset in buildRule.BuildAseet)
            {
                int index = 0;
                foreach (var assetLoadPath in buildAsset.AssetLoadPaths)
                {
                    if (assetLoadPath == path)
                    {
                        return buildAsset.AssetNames[index];
                    }
                    index++;
                }
            }
            return path;
        }
    }
}
#endif