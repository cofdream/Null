using UnityEngine;

namespace NullNamespace
{
    public interface IAssetLoad
    {
        //void Retain();
        //void Release();
    }

    public class AssetLoad : IAssetLoad
    {
        public string Name { get { return Asset.name; } }

        public Object Asset;

        private int referenceCount;

        public event System.Action<AssetLoad> OnUnload;

        public AssetLoad(Object asset)
        {
            referenceCount = 0;
            Asset = asset;
        }

        public void Retatin()
        {
            referenceCount++;
        }
        public void Release()
        {
            referenceCount--;

            if (referenceCount == 0)
            {
                if (Asset is GameObject)
                {
                    Asset = null;
                    //Resources.UnloadUnusedAssets();
                }
                else
                {
                    Resources.UnloadAsset(Asset);
                    Asset = null;
                }

                OnUnload?.Invoke(this);

                OnUnload = null;
            }
        }
    }

}