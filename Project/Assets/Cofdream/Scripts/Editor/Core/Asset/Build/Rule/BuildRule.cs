using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CofdreamEditor.Core.Asset
{
    public class BuildRule : ScriptableObject, IBuildRule
    {
        public string AssetPath;
        public string AssetBundleName;
        public string[] AssetNames;

        public int GetAssetBundleBuildCount => string.IsNullOrWhiteSpace(AssetPath) ? 0 : 1;

        public void CreateAssetBundleBuild(CreateCallback createCallback)
        {
            createCallback(new AssetBundleBuild()
            {
                assetBundleName = AssetBundleName,
                assetNames = AssetNames,
            });
        }
        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(AssetPath) == false) return;

            AssetBundleName = BuildRuleUtil.PathToAssetBundleName(AssetPath);
        }
    }
}
