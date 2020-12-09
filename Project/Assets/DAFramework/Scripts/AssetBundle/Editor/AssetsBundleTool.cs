﻿using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using DA.AssetsBundle;

internal static class AssetsBundleTool
{
    //todo 添加加载标记功能
    //private const string Mark_AssetBundle = "Assets/AssetBundle Mark";
    //static AssetsBundleTool()
    //{
    //    Selection.selectionChanged = OnSelectionChanged;
    //}

    //public static void OnSelectionChanged()
    //{
    //    var paths = DATools.Utils.GetSelectionPath();
    //    foreach (var path in paths)
    //    {

    //    }
    //    var selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
    //    var rules = BuildScript.GetOrCreateBuildRules();
    //    List<UnityEngine.Object> changeObject = new List<Object>();
    //    foreach (var o in selection)
    //    {
    //        var path = AssetDatabase.GetAssetPath(o);
    //        if (string.IsNullOrEmpty(path) || Directory.Exists(path))
    //        {
    //            continue;
    //        }

    //        bool Match(AssetBuild bundleAsset)
    //        {
    //            return bundleAsset.path.Equals(path);
    //        }
    //        var asset = ArrayUtility.Find(rules.assets, Match);
    //        if (asset != null)
    //        {
    //            SetChecked(patchIds, (int)asset.patch);
    //            //SetChecked(patchIds, (int)asset.patch);
    //        }
    //    }

    //    Debug.Log("Change");
    //}

    [MenuItem("DATool/AssetsBundle/Build Rules")]
    private static void BuildRules()
    {
        var watch = new Stopwatch();
        watch.Start();
        BuildScript.BuildRules();
        watch.Stop();
        Debug.Log("BuildBundles " + watch.ElapsedMilliseconds + " ms.");
    }

    [MenuItem("DATool/AssetsBundle/Build Bundles")]
    private static void BuildBundles()
    {
        var watch = new Stopwatch();
        watch.Start();
        BuildScript.BuildRules();
        BuildScript.BuildAssetBundles();
        watch.Stop();
        Debug.Log("BuildBundles " + watch.ElapsedMilliseconds + " ms.");
    }

    [MenuItem("DATool/AssetsBundle/Build Player")]
    private static void BuildStandalonePlayer()
    {
        var watch = new Stopwatch();
        watch.Start();
        BuildScript.BuildStandalonePlayer();
        watch.Stop();
        Debug.Log("BuildPlayer " + watch.ElapsedMilliseconds + " ms.");
    }

    [MenuItem("DATool/AssetsBundle/View/PersistentDataPath")]
    private static void ViewDataPath()
    {
        EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
    }


    #region AssetBundle Mask Tool
    const int priority = 1998 - 1;

    [MenuItem("Assets/GroupBy/Filename", false, priority = priority)]
    private static void GroupByFilename()
    {
        GroupAssets(GroupBy.Filename);
    }

    [MenuItem("Assets/GroupBy/Directory", false, priority = priority)]
    private static void GroupByDirectory()
    {
        GroupAssets(GroupBy.Directory);
    }

    [MenuItem("Assets/GroupBy/Explicit", false, priority = priority)]
    private static void GroupByExplicitLevel1()
    {
        GroupAssets(GroupBy.Explicit);
    }

    private static void GroupAssets(GroupBy nameBy)
    {
        var selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        var rules = BuildScript.GetOrCreateBuildRules();
        List<UnityEngine.Object> changeObject = new List<Object>();
        foreach (var o in selection)
        {
            var path = AssetDatabase.GetAssetPath(o);
            if (string.IsNullOrEmpty(path) || Directory.Exists(path))
            {
                continue;
            }
            rules.GroupAsset(path, nameBy);
        }
        EditorUtility.SetDirty(rules);
        AssetDatabase.ImportAsset(DA.AssetsBundle.BuildRules.AssetPath);
    }



    [MenuItem("Assets/PatchId/Level1", priority = priority)]
    private static void PatchByLevel1()
    {
        PatchAssets(PatchId.Level1);
    }

    [MenuItem("Assets/PatchId/Level2", priority = priority)]
    private static void PatchByLevel2()
    {
        PatchAssets(PatchId.Level2);
    }

    [MenuItem("Assets/PatchId/Level3", priority = priority)]
    private static void PatchByLevel3()
    {
        PatchAssets(PatchId.Level3);
    }

    [MenuItem("Assets/PatchId/Level4", priority = priority)]
    private static void PatchByLevel4()
    {
        PatchAssets(PatchId.Level4);
    }

    [MenuItem("Assets/PatchId/Level5", priority = priority)]
    private static void PatchByLevel5()
    {
        PatchAssets(PatchId.Level5);
    }

    private static void PatchAssets(PatchId patch)
    {
        var selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        var rules = BuildScript.GetOrCreateBuildRules();
        foreach (var o in selection)
        {
            var path = AssetDatabase.GetAssetPath(o);
            if (string.IsNullOrEmpty(path) || Directory.Exists(path))
            {
                continue;
            }
            rules.PatchAsset(path, patch);
        }

        EditorUtility.SetDirty(rules);
        AssetDatabase.SaveAssets();
    }




    [MenuItem("Assets/AssetBundle Mark/Remove", priority = priority)]
    private static void RemoveGroup()
    {
        var selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        var rules = BuildScript.GetOrCreateBuildRules();
        List<UnityEngine.Object> changeObject = new List<Object>();
        foreach (var o in selection)
        {
            var path = AssetDatabase.GetAssetPath(o);
            if (string.IsNullOrEmpty(path) || Directory.Exists(path))
            {
                continue;
            }
            rules.RemoveGroupAsset(path);
        }
        EditorUtility.SetDirty(rules);
        AssetDatabase.ImportAsset(DA.AssetsBundle.BuildRules.AssetPath);
    }

    [MenuItem("Assets/AssetBundle Mark/Show Info", priority = priority)]
    private static void CheckAssetBundleMark()
    {
        var selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        var rules = BuildScript.GetOrCreateBuildRules();
        foreach (var o in selection)
        {
            var path = AssetDatabase.GetAssetPath(o);
            if (string.IsNullOrEmpty(path) || Directory.Exists(path))
            {
                continue;
            }
            bool Match(AssetBuild bundleAsset)
            {
                return bundleAsset.path.Equals(path);
            }
            var asset = ArrayUtility.Find(rules.assets, Match);
            if (asset != null)
            {
                EditorUtility.DisplayDialog("已被标记", $"当前选中对象{o.name} 已被标记", "OK");
            }
            else
            {
                // todo 显示详细信息
                EditorUtility.DisplayDialog("未被标记", $"当前选中对象{o.name} 未被标记", "OK");
            }
        }
    }
    #endregion

    #region Tools

    [MenuItem("DATool/AssetsBundle/View/CRC")]
    private static void GetCRC()
    {
        var path = EditorUtility.OpenFilePanel("OpenFile", Environment.CurrentDirectory, "");
        if (string.IsNullOrEmpty(path)) return;

        using (var fs = File.OpenRead(path))
        {
            var crc = Utility.GetCRC32Hash(fs);
            Debug.Log(crc);
        }
    }

    [MenuItem("DATool/AssetsBundle/View/MD5")]
    private static void GetMD5()
    {
        var path = EditorUtility.OpenFilePanel("OpenFile", Environment.CurrentDirectory, "");
        if (string.IsNullOrEmpty(path)) return;

        using (var fs = File.OpenRead(path))
        {
            var crc = Utility.GetMD5Hash(fs);
            Debug.Log(crc);
        }
    }

    [MenuItem("DATool/AssetsBundle/Take a screenshot")]
    private static void Screenshot()
    {
        var path = EditorUtility.SaveFilePanel("截屏", null, "screenshot_", "png");
        if (string.IsNullOrEmpty(path)) return;

        ScreenCapture.CaptureScreenshot(path);
    }

    #endregion
}