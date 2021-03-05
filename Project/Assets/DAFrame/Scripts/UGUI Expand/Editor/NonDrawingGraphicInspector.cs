using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace NullNamespace
{
    [CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
    internal sealed class NonDrawingGraphicInspector : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Script);

            RaycastControlsGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
}