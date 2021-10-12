using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Cofdream.Core.Asset
{
    public class EditorAssetLoad
    {
        public const bool DefaultValue = true;
        public const string LOCAL_LOAD_MODEL = "EditorAssetLoad.localLoadModel";
        private const string MENI_ITEM_NAME = "Switch Editor Asset Load Model/LocalLoadModel";

        public static bool LocalLoadModel;

        static EditorAssetLoad()
        {
            LocalLoadModel = EditorPrefs.GetBool(LOCAL_LOAD_MODEL, DefaultValue);
        }


        [MenuItem(MENI_ITEM_NAME, true)]
        public static bool InitLocalLoadModel()
        {
            if (EditorApplication.isPlaying) return false;

            Menu.SetChecked(MENI_ITEM_NAME, LocalLoadModel);
            return true;
        }

        [MenuItem(MENI_ITEM_NAME)]
        public static void SwitchLocalLoadModel()
        {
            LocalLoadModel = !LocalLoadModel;
            EditorPrefs.SetBool(LOCAL_LOAD_MODEL, LocalLoadModel);

            Menu.SetChecked(MENI_ITEM_NAME, LocalLoadModel);
        }

        [MenuItem("1/TT")]
        public static void Temp()
        {
            GUIUtility.systemCopyBuffer = GUIUtility.systemCopyBuffer.Replace('/', '_');

            Resources.UnloadUnusedAssets();
            AssetBundle.UnloadAllAssetBundles(true);
        }

    }
}
#endif