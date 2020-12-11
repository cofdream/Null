using UnityEngine;
using UnityEditor;

namespace NullNamespace
{
    public class NewEditor : EditorWindow
    {
        public static GameObject A;
        public static GameObject B;

        [MenuItem("TestLoad/Load A Assets")]
        public static void AA()
        {

        }

        [MenuItem("TestLoad/Load B Assets")]
        public static void BB()
        {


        }

        [MenuItem("TestLoad/Load Unload Assets")]
        public static void Unload()
        {

        }

        [MenuItem("TestLoad/Debug")]
        public static void Debug()
        {

        }
    }
}