using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CofdreamEditor.Core.Asset
{
    public sealed class FolderBuildRule : ScriptableObject, IBuildRule
    {
        public Object AssetFolder;

        private string path;
        public int GetAssetBundleBuildCount()
        {
            path = AssetDatabase.GetAssetPath(AssetFolder);
            return string.IsNullOrEmpty(path) ? 0 : 1;
        }

        public void CreateAssetBundleBuild(CreateCallback createCallback)
        {
            createCallback(new AssetBundleBuild()
            {
                assetBundleName = BuildRuleUtil.PathToAssetBundleName(path),
                assetNames = new string[] { path },
            });
        }
    }
}