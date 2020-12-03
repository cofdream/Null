using System;
using UnityEngine;

namespace DA.Res
{
    public static class AssetBundleManager
    {
        public enum AssetBundleLoadMode : byte
        {
            Develop = 0,
            Simulation = 1,
            AssetBundle = 2,
        }
        public static AssetBundleLoadMode loadMode = AssetBundleLoadMode.Develop;
        static AssetBundleManager()
        {

        }

        public static void Load<T>(string url, Action<T, bool> callBack)
        {
            
        }

        public static void SetLoadMode(AssetBundleLoadMode loadMode)
        {
        }
    }
    public interface ILoader { };
    public class AssetsBundleLoder : ILoader { }

}