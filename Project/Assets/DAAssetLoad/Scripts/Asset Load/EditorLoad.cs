using UnityEngine;

#if UNITY_EDITOR
namespace DA.AssetLoad
{
    public class EditorLoad : IAssetLoad
    {
        private static ObjectPool.ObjectPool<EditorLoad> editorLoadPool;

        private string name;
        public string Name { get { return name; } }
        public Object Asset { get; private set; }
        public AssetLoadState LoadState { get; private set; }

        private int referenceCount;

        private System.Action<Object> onLoaded;

        private string assetPath;

        private System.Type loadType;

        private System.Action<string> unloadCallback;

        static EditorLoad()
        {
            editorLoadPool = new ObjectPool.ObjectPool<EditorLoad>();
            editorLoadPool.Initialize(() => new EditorLoad(), null, null, null);
        }
        public static EditorLoad GetEditorLoad(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            var editorLoad = editorLoadPool.Allocate();
            editorLoad.Initialize(path, loadType, unloadCallback);
            return editorLoad;
        }

        public void Initialize(string path, System.Type loadType, System.Action<string> unloadCallback)
        {
            referenceCount = 0;

            name = path;
            assetPath = path;

            LoadState = AssetLoadState.NotLoad;

            this.loadType = loadType;

            this.unloadCallback = unloadCallback;
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
            this.onLoaded += onLoaded;
            LoadAsset();

            this.onLoaded?.Invoke(Asset);
            this.onLoaded = null;
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

                unloadCallback?.Invoke(Name);

                Clear();

                editorLoadPool.Release(this);
            }
        }
        private void Clear()
        {
            name = null;
            Asset = null;
            assetPath = null;
            loadType = null;
            unloadCallback = null;
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