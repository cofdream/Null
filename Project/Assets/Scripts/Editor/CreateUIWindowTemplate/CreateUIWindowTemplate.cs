using UnityEngine;
using UnityEditor;
using DATools;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;

namespace NullNamespace
{
	public class CreateUIWindowTemplate :EditorWindow
	{
        // todo 创建一个文件夹
        private const string DEFINE_Window_NAME = "Window";
        private const string DEFINE_SCRIPT_TEMPLATE =
            "using UnityEngine;" +
            "\nusing UnityEngine.UI;" +
            "\n" +
            "\nnamespace DA.UI" +
            "\n{" +
            "\n\tpublic class #ScriptName# : UIWindow" +
            "\n\t{" +
            "\n\t\tObject bind;" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t\t" +
            "\n\t}" +
            "\n}";

        [MenuItem("Assets/Create/UIWindowTemplate", false, 82)]
        private static void OpenCreateUIWindowTemplate()
        {
            string pathName = Utils.GetSelectionPath()[0] + "/" + DEFINE_Window_NAME;
            Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

            var action = ScriptableObjectExpand.CreateInstanceOnly<CreateCSarpScriptAction>();
            action.ScriptTemplate = DEFINE_SCRIPT_TEMPLATE;

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, action, pathName, icon, string.Empty);
        }


        private class CreateCSarpScriptAction : EndNameEditAction
        {
            public string ScriptTemplate;
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var scriptName = Path.GetFileName(pathName);
                if (scriptName.Equals(DEFINE_Window_NAME)) return;

                pathName = pathName + ".cs";

                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                string script = GetScript(scriptName);

                script = IsClassOrInterface(script, scriptName);

                script = SetLineEndings(script, LineEndingsMode.OSNative);

                File.WriteAllText(pathName, script, encoding);

                AssetDatabase.ImportAsset(pathName);

                var obj = AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));

                ProjectWindowUtil.ShowCreatedAsset(obj);
            }

            private string GetScript(string scriptName)
            {
                return ScriptTemplate.Replace("#ScriptName#", scriptName);
            }

            private string IsClassOrInterface(string script, string scriptName)
            {
                if (scriptName.StartsWith("I"))
                {
                    if (scriptName.Length > 2)
                    {
                        var name = scriptName[1];
                        if (name > 'A' && name < 'Z')
                        {
                            script = script.Replace("class", "interface");
                        }
                    }
                }
                return script;
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