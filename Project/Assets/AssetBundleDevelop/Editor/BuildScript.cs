//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Build;
//using UnityEditor.Build.Reporting;

//namespace NullNamespace
//{
//    public class BuildScript : IPreprocessBuildWithReport
//    {
//        public int callbackOrder => throw new System.NotImplementedException();

//        public void OnPreprocessBuild(BuildReport report)
//        {
//            throw new System.NotImplementedException();
//        }

//        // 构建打包规则
//        internal static void BuildRules()
//        {
//            var rules = GetBuildRules();
//            rules.Build();
//        }
//        internal static BuildRules GetBuildRules()
//        {
//            return GetOrCreateAsset<BuildRules>("Assets/Rules.asset");
//        }



//        public static T GetOrCreateAsset<T>(string path) where T : ScriptableObject
//        {
//            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
//            if (asset == null)
//            {
//                asset = ScriptableObject.CreateInstance<T>();
//                AssetDatabase.CreateAsset(asset, path);
//                AssetDatabase.ImportAsset(path);
//            }

//            return asset;
//        }
//    }
//}