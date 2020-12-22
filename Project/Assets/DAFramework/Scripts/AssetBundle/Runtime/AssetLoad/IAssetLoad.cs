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

        bool Equals(string assetPath,string assetName);
        bool Equals(UnityEngine.Object asset);
        UnityEngine.Object LoadAsset(string assetPath, string assetName);
        void LoadAsync(string assetPath, string assetName, Action<UnityEngine.Object> loadCallBack);
        void Unload();
    }
}