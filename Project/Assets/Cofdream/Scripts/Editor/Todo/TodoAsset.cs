using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CofdreamEditor.Core
{
    public class TodoAsset : ScriptableObject
    {
        public Todo[] Todos;
    }

    [CustomEditor(typeof(TodoAsset)), CanEditMultipleObjects]
    public class TodoAssetInspector : Editor
    {
        string time;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            time = GUILayout.TextField(time);
            if (GUILayout.Button("GetTime"))
            {
                time = System.DateTime.Now.ToString("G");
            }
        }
    }
}