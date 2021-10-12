using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofdream.Core.Asset
{
    public class AssetsLoad
    {
        private AssetBundleLoad assetBundleLoad;
        private EditorAssetLoad editorAssetLoad;
        public AssetsLoad(string assetBundleName)
        {
            if (EditorAssetLoad.LocalLoadModel)
            {
                editorAssetLoad = EditorAssetLoad.Take(assetBundleName);
            }
            else
            {
                assetBundleLoad = AssetBundleLoad.Take(assetBundleName);
            }
        }

        // ?? change two func?
        public Object Load(string assetName, System.Type type)
        {
            if (EditorAssetLoad.LocalLoadModel)
            {
                return editorAssetLoad.LoadAsset(assetName, type);
            }
            else
            {
                return assetBundleLoad.LoadAsset(assetName, type);
            }
        }

        public void UnLoad()
        {
            if (EditorAssetLoad.LocalLoadModel)
            {
                EditorAssetLoad.Put(editorAssetLoad);
                editorAssetLoad = null;
            }
            else
            {
                AssetBundleLoad.Put(assetBundleLoad);
                assetBundleLoad = null;
            }
        }
    }
}