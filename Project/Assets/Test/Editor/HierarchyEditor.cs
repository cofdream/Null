using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class HierarchyEditor
{
    static List<Type> iconCmp = new List<Type>() { typeof(Canvas) };

    [MenuItem("Tools/Hierarchy")]
    public static void UpdateEnable()
    {
        EditorApplication.hierarchyWindowItemOnGUI = null;
        EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon;
    }

    private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject == null)
            return;

        Rect drawRect = GetFirstRect(selectionRect);

        for (int i = 0; i < iconCmp.Count; i++)
        {
            if (Draw(gameObject, drawRect, iconCmp[i]))
            {
                  drawRect.x -= drawRect.width;
            }
        }
        //Debug.Log("TTS  " + instanceID);

    }

    static string value;
    private static bool Draw(GameObject go, Rect drawRect, Type type)
    {
        var a = go.GetComponent(type);
        if (a != null && a.gameObject.activeInHierarchy && a is Canvas)//&& a.gameObject.name== "AAAA" && a is Canvas)
        {
            var can = a as Canvas;

            if (a.gameObject.name != "Canvas")
            {
                string str = can.name.ToString();
                string str1 = str;
                //GUIStyle gUIStyle = null;//new GUIStyle(GUI.skin.textField) { normal = { textColor = go.gameObject.GetComponent<UnityEngine.UI. Image>().color } }; ;
                //drawRect.width = (str.Length + 1) * 8;

                str1 = GUI.TextField(drawRect, str);

                // Debug.Log(str);
                int sort;

                if (go.gameObject.name == "Image (2)")
                {
                    Debug.Log(11);
                }

                if (str != str1)
                {

                    if (go.gameObject.name == "Image (2)")
                    {
                        Debug.Log(22);
                    }

                    if (int.TryParse(str1, out sort))
                    {

                    }
                    can.name = str1;
                    can.GetComponent<Test2222>().CallBack(str,str1);
                    EditorUtility.SetDirty(can);
                    Debug.Log(sort + "   " + a.GetInstanceID());
                }
                else
                {
                    Debug.LogError("Equip");
                }
            }
            else
            {

                string str = can.sortingOrder.ToString();
                str = GUI.TextField(drawRect, str);
                // Debug.Log(str);
                int sort;
                if (int.TryParse(str, out sort))
                {
                    can.sortingOrder = sort;
                    EditorUtility.SetDirty(can);
                }
            }
            return true;
        }
        return false;
    }


    private static Rect GetFirstRect(Rect selectionRect)
    {
        Rect rect = new Rect(selectionRect);
        rect.x += rect.width - rect.height * 3;
        rect.width = rect.height * 3;
        return rect;
    }

    private static void Update()
    {

        EditorApplication.RepaintHierarchyWindow();
        // Debug.Log("TTS");

    }

}
