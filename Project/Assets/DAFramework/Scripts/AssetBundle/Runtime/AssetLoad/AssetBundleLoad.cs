using System;
using UnityEngine;

namespace DA
{
    public class AssetBundleLoad : IAssetLoad
    {
        private AssetBundle assetBundle;
        private string assetPath;
        private ushort refNumber;

        private AssetBundleCreateRequest createRequest;
        private Action<UnityEngine.Object> onLoaded;

        private bool isLoaded;

        public AssetBundleLoad()
        {
            Reset();
        }

        private void Reset()
        {
            assetBundle = null;
            assetPath = null;
            refNumber = 0;
            createRequest = null;
            onLoaded = null;
            isLoaded = false;
        }

        public bool Equals(string path)
        {
            return assetPath.Equals(path);
        }
        public bool Equals(UnityEngine.Object asset)
        {
            return this.assetBundle.Equals(asset);
        }
        public T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            if (assetBundle == null)
            {
                if (isLoaded)
                    return null; //throw new Exception("当前Asset 已加载过,不要重复加载");

                isLoaded = true;

                assetPath = path;
                assetBundle = AssetBundle.LoadFromFile(path);
            }


            return Load() as T;
        }
        public void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object
        {
            if (assetBundle != null)
            {
                T temp = Load() as T;
                loadCallBack?.Invoke(temp);
                return;
            }
            Delegate va = loadCallBack;

            va.DynamicInvoke();

            if (isLoaded)
                return;//throw new Exception("当前Asset 已加载过,不要重复加载");
            isLoaded = true;

            assetPath = path;
            createRequest = AssetBundle.LoadFromFileAsync(path);
            createRequest.completed += ABLoadCallBack;
        }
        private void ABLoadCallBack(AsyncOperation asyncOperation)
        {
            assetBundle = createRequest.assetBundle;
            var temp = Load();
            onLoaded?.Invoke(temp);

            createRequest = null;
        }

        private UnityEngine.Object Load()
        {
            refNumber++;
            return assetBundle;
        }

        public void Unload()
        {
            refNumber--;
            if (refNumber == 0)
            {
                assetBundle.Unload(true);
            }
        }
    }
}