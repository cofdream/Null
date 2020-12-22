using System;

namespace DA
{
    public enum AssetLoadState : ushort
    {
        NotLoaded = 0,
        Loading = 1,
        Loaded = 2,
        Unload = 3,
    }

    public interface IAssetLoad
    {
        event Action<IAssetLoad> UnloadCallback;

        bool Equals(string assetPath);
        bool Equals(UnityEngine.Object asset);
        UnityEngine.Object LoadAsset(string assetPath);
        void LoadAsync(string assetPath, Action<UnityEngine.Object> loadCallBack);
        void Unload();
    }
}