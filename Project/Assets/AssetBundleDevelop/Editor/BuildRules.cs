//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;
//using System;
//using System.IO;
//using System.Text;

//namespace NullNamespace
//{
//    public enum GroupBy
//    {
//        None,
//        Explicit,
//        Filename,
//        Directory,
//    }

//    [Serializable]
//    public class AssetBuild
//    {
//        public string name;
//        public string group;
//        public string bundle = string.Empty;
//        public int id;
//        public GroupBy groupBy = GroupBy.Filename;
//    }


//    public class BuildRules : ScriptableObject
//    {
//        private readonly List<string> _duplicated = new List<string>();
//        private readonly Dictionary<string, HashSet<string>> _tracker = new Dictionary<string, HashSet<string>>();
//        private readonly Dictionary<string, string> _asset2Bundles = new Dictionary<string, string>();
//        private readonly Dictionary<string, string> _unexplicits = new Dictionary<string, string>();

//        [Header("版本号")]
//        [Tooltip("构建的版本号")] public int build;
//        public int major;
//        public int minor;

//        [Header("自动分包分组配置")]
//        [Tooltip("是否自动记录资源的分包分组")]
//        public bool autoRecord = false;
//        [Tooltip("按目录自动分组")]
//        public string[] autoGroupByDirectories = new string[0];

//        [Header("编辑器提示选项")]
//        [Tooltip("检查加载路径大小写是否存在")]
//        public bool validateAssetPath;

//        [Header("首包内容配置")]
//        [Tooltip("是否整包")] public bool allAssetsToBuild;
//        [Tooltip("首包包含的分包")] public string[] patchesInBuild = new string[0];
//        [Tooltip("BuildPlayer的时候被打包的场景")] public SceneAsset[] scenesInBuild = new SceneAsset[0];

//        [Header("AB打包配置")]
//        [Tooltip("AB的扩展名")] public string extension = "";
//        public bool nameByHash;
//        [Tooltip("打包AB的选项")] public BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;

//        [Header("缓存数据")]
//        [Tooltip("所有要打包的资源")] public List<AssetBuild> assets = new List<AssetBuild>();
//        [Tooltip("所有分包")] public List<PatchBuild> patches = new List<PatchBuild>();
//        [Tooltip("所有打包的资源")] public List<BundleBuild> bundles = new List<BundleBuild>();


//        private string GetGroupName(AssetBuild assetBuild)
//        {
//            return GetGroupName(assetBuild.groupBy, assetBuild.name, assetBuild.group);
//        }
//        private bool IsScene(string asset)
//        {
//            return asset.EndsWith(".unity");
//        }

//        // 获取分组名称
//        private string GetGroupName(GroupBy groupBy, string asset, string group = null, bool isChildren = false, bool isShared = false)
//        {
//            //对shader scene 文件特殊处理
//            if (asset.EndsWith(".shader"))
//            {
//                group = "shaders";
//                groupBy = GroupBy.Explicit;
//                isChildren = false;
//            }
//            else if (IsScene(asset))
//            {
//                groupBy = GroupBy.Filename;
//            }

//            switch (groupBy)
//            {
//                case GroupBy.Explicit:
//                    break;
//                case GroupBy.Filename:
//                    {
//                        var assetName = Path.GetFileNameWithoutExtension(asset);
//                        var directoryName = Path.GetDirectoryName(asset);
//                        var sb = new StringBuilder(assetName + "_");
//                        var dir = new DirectoryInfo(directoryName);
//                        int max = 1, i = 0;
//                        while (i < max)
//                        {
//                            if (dir == null)
//                            {
//                                break;
//                            }

//                            sb.Insert(0, dir.Name + "_");
//                            dir = dir.Parent;
//                            ++i;
//                        }

//                        group = sb.ToString();
//                    }
//                    break;
//                case GroupBy.Directory:
//                    {
//                        var directoryName = Path.GetDirectoryName(asset);
//                        var sb = new StringBuilder();
//                        var dir = new DirectoryInfo(directoryName);
//                        int max = 3, i = 0;
//                        while (i < max)
//                        {
//                            if (dir == null)
//                            {
//                                break;
//                            }

//                            sb.Insert(0, dir.Name + "_");
//                            dir = dir.Parent;
//                            if (dir.FullName == System.Environment.CurrentDirectory)
//                            {
//                                break;
//                            }
//                            ++i;
//                        }

//                        group = sb.ToString();
//                        break;
//                    }
//            }
//            if (isChildren)
//            {
//                return "children_" + group;
//            }

//            if (isShared)
//            {
//                group = "shared_" + group;
//            }
//            return (nameByHash ? Utility.GetMD5Hash(group) : group.TrimEnd('_').ToLower()) + extension;
//        }

//        // 验证资源是否符合调节
//        internal bool ValidateAsset(string asset)
//        {
//            if (!asset.StartsWith("Assets/"))
//            {
//                Debug.LogWarning($"资源 {asset} 不在Assets文件夹下,请检查。");
//                return false;
//            }

//            var ext = Path.GetExtension(asset).ToLower();
//            if (ext != ".dll" && ext != ".cs" && ext != ".meta" && ext != ".js" && ext != ".boo")
//            {
//                Debug.LogWarning($"资源 {asset} 文件是 dlll cs  meta js boo 中的一种，请检查。");
//                return true;
//            }
//            return false;
//        }
//        private void BundleAsset(string assetName, string assetBundleName)
//        {
//            if (IsScene(assetName))
//            {
//                assetBundleName = GetGroupName(GroupAsset(assetName));
//            }

//            _asset2Bundles[assetName] = assetBundleName;
//        }

//        public void Build()
//        {
//            Clear();
//            CollectAssets();
//            AnalysisAssets();
//            OptimizeAssets();
//            Save();
//        }
//        // 收集资源
//        private void CollectAssets()
//        {
//            var list = new List<AssetBuild>();
//            var len = Environment.CurrentDirectory.Length + 1;

//            for (var index = 0; index < assets.Count; index++)
//            {
//                var asset = assets[index];
//                var path = new FileInfo(asset.name);
//                if (path.Exists)
//                {
//                    if (ValidateAsset(asset.name))
//                    {
//                        var relativePath = path.FullName.Substring(len).Replace("\\", "/");
//                        if (!relativePath.Equals(asset.name))
//                        {
//                            Debug.LogWarningFormat("检查到路径大小写不匹配！输入：{0}实际：{1}，已经自动修复。", asset.name, relativePath);
//                            asset.name = relativePath;
//                        }
//                        list.Add(asset);
//                    }
//                }
//                else
//                {
//                    Debug.LogWarning($"资源 {path} 不存在。");
//                }
//            }

//            for (var i = 0; i < list.Count; i++)
//            {
//                var asset = list[i];
//                if (asset.groupBy == GroupBy.None)
//                {
//                    continue;
//                }

//                asset.bundle = GetGroupName(asset);

//                BundleAsset(asset.name, asset.bundle);
//            }

//            assets = list;
//        }


//        private void Clear()
//        {
//            _unexplicits.Clear();
//            _tracker.Clear();
//            _duplicated.Clear();
//            _asset2Bundles.Clear();
//        }

//    }
//}