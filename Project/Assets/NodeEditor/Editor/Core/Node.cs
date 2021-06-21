using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class NodeReferencePoint
    {
        public Rect PointRect;

        public int ReferenceId;
    }
    public class Node
    {
        public Rect NodeRect;
        public bool IsDragged;

        public GUIStyle NodeGUIStyle;

        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;

        public NodeReferencePoint SelfPoint;
        public NodeReferencePoint FriendPoint;

        public Action<Node> OnRemoveNode;

        public Person Person;

        protected static GUISkin NodeSkin;
        static Node()
        {
            NodeSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/NodeEditor/PersonGUISkin.guiskin");
        }

        public Node(Vector2 position, GUIStyle nodeStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
        {
            Person = new Person();
            Person.Name = "dasda";
            Person.Age = 123;
            Person.Friend = null;


            NodeRect = new Rect(position.x, position.y, 250, 150);
            NodeGUIStyle = nodeStyle;

            SelfPoint = new NodeReferencePoint();
            FriendPoint = new NodeReferencePoint();

            SelfPoint.PointRect = NodeRect;
            SelfPoint.PointRect.width = 18;
            SelfPoint.PointRect.height = 18;

            //FriendPoint.ReferenceId = Person.Friend.InstanceID;

            InPoint = new ConnectionPoint(SelfPoint, ConnectionPointType.In, inPointStyle, onClickInPoint);
            OutPoint = new ConnectionPoint(FriendPoint, ConnectionPointType.Out, outPointStyle, onClickOutPoint);
            OnRemoveNode = onRemoveNode;
        }

        public void Drag(Vector2 delta)
        {
            NodeRect.position += delta;
            SelfPoint.PointRect.position += delta;
        }

        public void Draw()
        {
            var rect = NodeRect;

            GUI.Box(rect, GUIContent.none, NodeSkin.box);

            var rectTitle = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Label(rectTitle, typeof(Person).Name, NodeSkin.customStyles[3]);

            var nameRect = new Rect(rect.x + 2f, rect.y + rectTitle.height + 5, rect.width - 4f, 18f);
            Person.Name = DrawVariable(nameRect, "Name", Person.Name);

            var ageRect = new Rect(rect.x + 2f, nameRect.y + nameRect.height, rect.width - 4f, 18f);
            Person.Age = DrawVariable(ageRect, "Age", Person.Age);

            var friendRect = new Rect(rect.x + 2f, ageRect.y + ageRect.height, rect.width - 4f, 18f);
            Person.Friend = DrawVariable(friendRect, "Friend", Person.Friend);

            InPoint.Draw();
            OutPoint.Draw();
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


        private string DrawVariable(Rect rect, string name, string value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);
            Rect rightRect = new Rect(rect.x + width, rect.y, width, rect.height);

            GUI.Label(leftRect, name);

            var outValue = GUI.TextField(rightRect, value.ToString());

            return outValue;
        }
        private int DrawVariable(Rect rect, string name, int value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);
            Rect rightRect = new Rect(rect.x + width, rect.y, width, rect.height);

            GUI.Label(leftRect, name);

            var outValue = GUI.TextField(rightRect, value.ToString());

            if (int.TryParse(outValue, out int result))
                return result;
            return value;
        }

        private Person DrawVariable(Rect rect, string name, Person value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);

            Rect rightRect = new Rect(rect.x + rect.width - 18, rect.y, 18, rect.height);

            GUI.Label(leftRect, name);

            FriendPoint.PointRect = rightRect;

            return value;
        }
    }
}