using System.Collections.Generic;
using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetLoader
    {
        private static ObjectPool.ObjectPool<AssetLoader> asserloaderPool;

        private static List<IAssetLoad> sharedAssetLoadList = new List<IAssetLoad>();

        private List<IAssetLoad> assetLoadList = new List<IAssetLoad>();

        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, typeof(T));
                assetLoadList.Add(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset();

            return assetLoad.Asset as T;
        }
        public void LoadAssetSync<T>(string assetName, System.Action<T> onLoaded) where T : UnityEngine.Object
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, typeof(T));
                assetLoadList.Add(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAssetSync((asset) =>
            {
                onLoaded?.Invoke(asset as T);
            });
        }
        public void LoadAssetBundle(string assetName)
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, null);
                assetLoadList.Add(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAsset();
        }
        public void LoadAssetBundleSync(string assetName, System.Action<Object> onLoaded)
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetName);

            if (assetLoad == null)
            {
                assetLoad = CreateIAssetLoad(assetName, null);
                assetLoadList.Add(assetLoad);
            }

            assetLoad.Retain();
            assetLoad.LoadAssetSync(onLoaded);
        }


        private IAssetLoad GetIAssetLoad(string assetName)
        {
            IAssetLoad assetLoad = GetIAssetLoad(assetLoadList, assetName);
            if (assetLoad != null)
                return assetLoad;

            assetLoad = GetIAssetLoad(sharedAssetLoadList, assetName);
            if (assetLoad != null)
            {
                assetLoadList.Add(assetLoad);
                return assetLoad;
            }

            return null;
        }

        public void Unload(string assetName)
        {
            for (int i = 0; i < assetLoadList.Count; i++)
            {
                if (assetLoadList[i].Name == assetName)
                {
                    assetLoadList.RemoveAt(i);
                    return;
                }
            }

            Debug.LogError("卸载资源失败:" + assetName);
        }

        public void UnloadAll()
        {
            foreach (var item in assetLoadList)
            {
                item.Release();
            }
            assetLoadList.Clear();
        }

        public void ReleaseAssetLoader()
        {
            asserloaderPool.Release(this);
        }

        static AssetLoader()
        {
            var pool = new ObjectPool.ObjectPool<AssetLoader>();
            pool.Initialize(() => new AssetLoader(), null, null, null);

            asserloaderPool = pool;
        }

        public static AssetLoader GetAssetLoader()
        {
            return asserloaderPool.Allocate();
        }

        private static IAssetLoad GetIAssetLoad(IEnumerable<IAssetLoad> collection, string assetName)
        {
            foreach (var item in collection)
                if (item.Name == assetName)
                    return item;
            return null;
        }
        private static IAssetLoad CreateIAssetLoad(string assetName, System.Type loadType)
        {
            IAssetLoad assetLoad = null;
#if UNITY_EDITOR
            if (assetName.StartsWith("resources://"))
            {
                assetLoad = ResourcesrLoad.GetLoad(assetName, loadType, RemoveSharedAssetLoad);
            }
            else if (AssetLoadManager.IsSimulationMode == false)
            {
                assetLoad = EditorLoad.GetEditorLoad(assetName, loadType, RemoveSharedAssetLoad);
            }
            else
#endif
            {
                if (assetName.EndsWith(".ab"))
                {
                    assetLoad = AssetBundleLoad.GetAssetBundleLoad(assetName, RemoveSharedAssetLoad);
                }
                else
                {
                    assetLoad = AssetLoad.GetAssetLoad(assetName, loadType, RemoveSharedAssetLoad);
                }
            }

            sharedAssetLoadList.Add(assetLoad);

            return assetLoad;
        }
        private static void RemoveSharedAssetLoad(string assetName)
        {
            for (int i = 0; i < sharedAssetLoadList.Count; i++)
            {
                if (sharedAssetLoadList[i].Name == assetName)
                {
                    sharedAssetLoadList.RemoveAt(i);
                    return;
                }
            }
            Debug.LogError("卸载共享资源失败:" + assetName);
        }
    }
}