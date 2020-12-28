using UnityEngine;

namespace NullNamespace
{
    public class EditorLoad : IAssetLoad
    {
        private string name;
        public string Name { get { return name; } }

        public Object Asset { get; private set; }

        private int referenceCount;

        public event System.Action<Object> OnLoaded;
        public event System.Action<IAssetLoad> OnUnload;

        private ResourceRequest resourceRequest;

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

                Asset = UnityEditor.AssetDatabase.LoadAssetAtPath(this.assetPath, loadType);

                LoadState = AssetLoadState.Loaded;
            }
        }

        public void LoadAssetSync(System.Action<Object> onLoaded)
        {
            switch (LoadState)
            {
                case AssetLoadState.NotLoad:

                    string res = "Assets/Resources/";
                    if (assetPath.StartsWith(res))
                    {
                        var temp = assetPath.Substring(res.Length);
                        int index = temp.LastIndexOf('.');
                        if (index > 0)
                        {
                            temp = temp.Substring(0, index);
                        }

                        resourceRequest = Resources.LoadAsync(temp, loadType);
                        resourceRequest.completed += ResourceRequest_completed;

                        OnLoaded += onLoaded;
                    }
                    else
                    {
                        LoadState = AssetLoadState.Loading;

                        new DA.Timer.TimerOnce(() =>
                        {
                            Asset = UnityEditor.AssetDatabase.LoadAssetAtPath(this.assetPath, loadType);

                            LoadState = AssetLoadState.Loaded;

                            LoadAsset();
                            OnLoaded?.Invoke(Asset);
                            OnLoaded = null;

                        }, 0.0001f).Run();

                        OnLoaded += onLoaded;
                    }

                    break;
                case AssetLoadState.Loading:
                    OnLoaded += onLoaded;
                    break;
                case AssetLoadState.Loaded:
                    onLoaded?.Invoke(Asset);
                    break;
            }


        }

        private void ResourceRequest_completed(AsyncOperation obj)
        {
            Asset = resourceRequest.asset;
            resourceRequest = null;

            OnLoaded?.Invoke(Asset);
            OnLoaded = null;
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

                OnUnload?.Invoke(this);
                OnUnload = null;
            }
        }

    }
}