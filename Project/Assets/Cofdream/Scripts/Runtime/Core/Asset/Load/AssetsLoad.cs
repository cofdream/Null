using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofdream.Core.Asset
{
    public class AssetsLoad
    {
        private AssetBundleLoad assetBundleLoad;

        public AssetsLoad(string assetBundleName)
        {
            assetBundleLoad = AssetBundleLoad.Take(assetBundleName);
        }

        // ?? change two func?
        public object Load(string assetName, System.Type type)
        {
            return assetBundleLoad.LoadAsset(assetName, type);
        }

        public void UnLoad()
        {
            AssetBundleLoad.Put(assetBundleLoad);
            assetBundleLoad = null;
        }
    }
}