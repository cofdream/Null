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
        event Action<IAssetLoad> UnloadCallBack;

        bool Equals(string path, string name);
        bool Equals(UnityEngine.Object asset);
        T LoadAsset<T>(string path, string name) where T : UnityEngine.Object;
        void LoadAsync<T>(string path, string name, Action<T> loadCallBack) where T : UnityEngine.Object;
        void Unload();
    }
}