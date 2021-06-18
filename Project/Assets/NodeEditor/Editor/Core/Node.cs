using System;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class Node
    {
        public Rect NodeRect;
        public string Title;
        public bool IsDragged;

        public GUIStyle NodeGUIStyle;

        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;

        public Action<Node> OnRemoveNode;

        public Person Person;


        public GUISkin NodeSkin;

        public Node(Vector2 position, GUIStyle nodeStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
        {
            NodeRect = new Rect(position.x, position.y, 250, 150);
            NodeGUIStyle = nodeStyle;

            InPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, onClickInPoint);
            OutPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, onClickOutPoint);
            OnRemoveNode = onRemoveNode;

            NodeSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/NodeEditor/PersonGUISkin.guiskin");

            Person = new Person();
            Person.Name = "dasda";
            Person.Age = 123;
            Person.Friend = null;
        }

        public void Drag(Vector2 delta)
        {
            NodeRect.position += delta;
        }

        public void Draw()
        {
            InPoint.Draw();
            OutPoint.Draw();

            var rect = NodeRect;
            //GUI.Box(rect, "Person", NodeSkin.box);

            if (Person == null)
            {
                Person = new Person();
                Person.Name = "dasda";
                Person.Age = 123;
                Person.Friend = null;
            }

            GUILayout.BeginArea(rect, NodeSkin.box);
            {
                //GUILayout.BeginHorizontal();
                //GUILayout.FlexibleSpace();
                GUILayout.Label(typeof(Person).Name,NodeSkin.label);
                //GUILayout.FlexibleSpace();
                //GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Name");
                Person.Name = GUILayout.TextField(Person.Name);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();


            //serializedObject.dwr

            //var PersonRect = new Rect(rect.x + 10, rect.y + 18, rect.width - 20, 18 * 5);
            //Person.Name = GUI.TextField(PersonRect, Person.Name, NodeSkin.textField);
        }
        public bool ProcessEvents(UnityEngine.Event _event)
        {
            switch (_event.type)
            {
                case EventType.MouseDown:
                    if (_event.button == 0)
                    {
                        if (NodeRect.Contains(_event.mousePosition))
                        {
                            IsDragged = true;
                        }
                        else
                        {
                        }
                        GUI.changed = true;

                    }

                    if (_event.button == 1 && NodeRect.Contains(_event.mousePosition))
                    {
                        ProcessContextMenu();
                        _event.Use();
                    }
                    break;
                case EventType.MouseUp:
                    IsDragged = false;
                    break;
                case EventType.MouseDrag:
                    if (IsDragged && _event.button == 0)
                    {
                        Drag(_event.delta);
                        _event.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        private void OnClickRemoveNode()
        {
            OnRemoveNode?.Invoke(this);
        }
    }
}