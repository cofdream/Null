using UnityEngine;

namespace DA.AssetsBundle
{
    public static class AssetUtil
    {
        /// <summary>
        /// 返回本地文件根目录
        /// 编辑器环境     文件存放 Assets                         同级目录下(工程文件夹下)
        /// 正式环境(出包) 文件存放 Application.persistentDataPath 下</summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetProjectPath(string path)
        {
#if UNITY_EDITOR
            if (AssetLoader.IsSimulationMode == false)
                return $"{System.IO.Directory.GetParent(Application.dataPath).FullName}/{path}";
            else
#endif
                return $"{Application.persistentDataPath}/{path}";

        }





        /// <summary>
        /// 返回运行平台文件夹名称
        /// </summary>
        /// <param name="runtimePlatform"></param>
        /// <returns></returns>
        public static string GetPlatform(RuntimePlatform runtimePlatform)
        {
            switch (runtimePlatform)
            {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";

#if UNITY_EDITOR
                case RuntimePlatform.WindowsEditor:
#endif
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                    return "OSX";
                case RuntimePlatform.LinuxPlayer:
                    return "Linux";


            }
            return runtimePlatform.ToString();
        }

#if UNITY_EDITOR
        /// <summary>
        /// 返回编辑器模式Build平台文件夹名称
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <returns></returns>
        public static string GetPlatform(UnityEditor.BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";

                case UnityEditor.BuildTarget.iOS:
                    return "iOS";

                case UnityEditor.BuildTarget.Android:
                    return "Android";

                case UnityEditor.BuildTarget.WebGL:
                    return "WebGL";

#if UNITY_2019_2_OR_NEWER

                case UnityEditor.BuildTarget.StandaloneLinux64:
#else
                case UnityEditor.BuildTarget.StandaloneLinux:
                case UnityEditor.BuildTarget.StandaloneLinuxUniversal:
#endif
                    return "Linux";

#if UNITY_2017_3_OR_NEWER
                case UnityEditor.BuildTarget.StandaloneOSX:
#else
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
#endif
                    return "OSX";
            }
            return buildTarget.ToString();
        }
#endif
    }
}