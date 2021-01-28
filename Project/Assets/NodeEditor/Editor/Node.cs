using System;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class Node
    {
        public Rect Rect;
        public string Title;
        public bool IsDragged;

        public GUIStyle NodeGUIStyle;

        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;

        public Action<Node> OnRemoveNode;

        public Node(Vector2 position, float width, float height, GUIStyle nodeStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
        {
            Rect = new Rect(position.x, position.y, width, height);
            NodeGUIStyle = nodeStyle;

            InPoint = new ConnectionPoint(this,ConnectionPointType.In,inPointStyle, onClickInPoint);
            OutPoint = new ConnectionPoint(this,ConnectionPointType.Out, outPointStyle, onClickOutPoint);
            OnRemoveNode = onRemoveNode;
        }

        public void Drag(Vector2 delta)
        {
            Rect.position += delta;
        }

        public void Draw()
        {
            InPoint.Draw();
            OutPoint.Draw();

            GUI.Box(Rect, Title, NodeGUIStyle);
        }
        public bool ProcessEvents(UnityEngine.Event _event)
        {
            switch (_event.type)
            {
                case EventType.MouseDown:
                    if (_event.button == 0)
                    {
                        if (Rect.Contains(_event.mousePosition))
                        {
                            IsDragged = true;
                        }
                        else
                        {
                        }
                        if (!GUI.changed) GUI.changed = true;

                    }

                    if (_event.button == 1 && Rect.Contains(_event.mousePosition))
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