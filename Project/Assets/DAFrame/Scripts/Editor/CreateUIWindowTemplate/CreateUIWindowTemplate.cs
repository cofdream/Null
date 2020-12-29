using UnityEngine;
using UnityEditor;
using DATools;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;

namespace NullNamespace
{
    public class CreateUIWindowTemplate : EditorWindow
    {
        private const string DEFINE_Window_NAME = "Window";
        private static string DEFINE_SCRIPT_TEMPLATE_PATH = Application.dataPath + @"\Scripts\Editor\CreateUIWindowTemplate\UIWindowTemplate.txt";

        [MenuItem("Assets/Create/UIWindow/WindowTemplate", false, 82)]
        private static void OpenCreateUIWindowTemplate()
        {
            if (File.Exists(DEFINE_SCRIPT_TEMPLATE_PATH) == false) return;

            string pathName = Utils.GetSelectionPath()[0] + "/" + DEFINE_Window_NAME;
            Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

            var action = ScriptableObjectExpand.CreateInstanceOnly<CreateCSarpScriptAction>();
            action.ScriptTemplate = File.ReadAllText(DEFINE_SCRIPT_TEMPLATE_PATH);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, action, pathName, icon, string.Empty);
        }

        private static bool isCreateFolder = false;
        [MenuItem("Assets/Create/UIWindow/WindowTemplateFolder", false, 82)]
        private static void OpenCreateUIWindonFolder()
        {
            isCreateFolder = true;
            OpenCreateUIWindowTemplate();
        }

        private class CreateCSarpScriptAction : EndNameEditAction
        {
            public string ScriptTemplate;
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var scriptName = Path.GetFileName(pathName);
                if (scriptName.Equals(DEFINE_Window_NAME)) return;

                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                string script = GetScript(scriptName);

                script = SetLineEndings(script, LineEndingsMode.OSNative);

                pathName = CreateFolder(pathName, scriptName);

                pathName = pathName + ".cs";

                File.WriteAllText(pathName, script, encoding);

                AssetDatabase.ImportAsset(pathName);

                var obj = AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));

                ProjectWindowUtil.ShowCreatedAsset(obj);
            }

            private string GetScript(string scriptName)
            {
                return ScriptTemplate.Replace("#ScriptName#", scriptName);
            }

            private string CreateFolder(string path, string scriptName)
            {
                if (isCreateFolder)
                {
                    isCreateFolder = false;
                    string rootPath = path + "/Window/";
                    Directory.CreateDirectory(rootPath);
                    return rootPath + scriptName;
                }
                return path;
            }

            private string SetLineEndings(string content, LineEndingsMode lineEndingsMode)
            {
                string replacement;
                switch (lineEndingsMode)
                {
                    case LineEndingsMode.OSNative:
                        replacement = ((Application.platform != RuntimePlatform.WindowsEditor) ? "\n" : "\r\n");
                        break;
                    case LineEndingsMode.Unix:
                        replacement = "\n";
                        break;
                    case LineEndingsMode.Windows:
                        replacement = "\r\n";
                        break;
                    default:
                        replacement = "\n";
                        break;
                }

                content = Regex.Replace(content, "\\r\\n?|\\n", replacement);
                return content;
            }
        }
    }
}