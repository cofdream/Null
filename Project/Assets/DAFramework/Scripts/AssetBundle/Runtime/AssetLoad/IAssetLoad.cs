using System;

namespace DA
{
    interface IAssetLoad
    {
        bool Equals(string path);
        bool Equals(UnityEngine.Object asset);
        T LoadAsset<T>(string path) where T : UnityEngine.Object;
        void LoadAsync<T>(string path, Action<T> loadCallBack) where T : UnityEngine.Object;
        void Unload();
    }
}