using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace CofdreamEditor.Core.Asset
{
    public sealed class AllFolderBuildRule : ScriptableObject, IBuildRule
    {
        public Object AssetFolder;
        //Cache
        private string[] folders;

        public int GetAssetBundleBuildCount()
        {
            if (AssetFolder != null)
            {
                string path = AssetDatabase.GetAssetPath(AssetFolder);
                if (AssetDatabase.IsValidFolder(path))
                {
                    folders = AssetDatabase.GetSubFolders(path);
                    return folders.Length;
                }
            }
            return 0;
        }

        public void CreateAssetBundleBuild(CreateCallback createCallback)
        {
            for (int i = 0; i < folders.Length; i++)
            {
                createCallback(new AssetBundleBuild()
                {
                    assetBundleName = BuildRuleUtil.PathToAssetBundleName(folders[i]),
                    assetNames = new string[] { folders[i] },
                });
            }

            folders = null;
        }

        private void OnValidate()
        {
            if (AssetFolder == null) return;

            string path = AssetDatabase.GetAssetPath(AssetFolder);
            if (AssetDatabase.IsValidFolder(path) == false)
            {
                EditorUtility.DisplayDialog("Warring", "当前对象不是文件夹路径，请给文件夹路径", "确认");
                return;
            }
        }
    }
}
