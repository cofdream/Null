using Game.FSM;
using NullNamespace;
using System;
using System.Linq;
using System.Reflection;
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

        protected static GUISkin NodeSkin;
        static Node()
        {
            NodeSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/NodeEditor/PersonGUISkin.guiskin");
        }

        public Node(Vector2 position, GUIStyle nodeStyle, GUIStyle inPointStyle, GUIStyle outPointStyle,
            Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
        {
            NodeRect = new Rect(position.x, position.y, 250, 150);
            NodeGUIStyle = nodeStyle;

            SelfPoint = new NodeReferencePoint();
            FriendPoint = new NodeReferencePoint();

            SelfPoint.PointRect = NodeRect;
            SelfPoint.PointRect.width = 18;
            SelfPoint.PointRect.height = 18;


            InPoint = new ConnectionPoint(SelfPoint, ConnectionPointType.In, inPointStyle, onClickInPoint);
            OutPoint = new ConnectionPoint(FriendPoint, ConnectionPointType.Out, outPointStyle, onClickOutPoint);
            OnRemoveNode = onRemoveNode;
        }

        public void Drag(Vector2 delta)
        {
            NodeRect.position += delta;
            SelfPoint.PointRect.position += delta;
        }

        public virtual void Draw()
        {

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


        protected string DrawVariable(Rect rect, string name, string value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);
            Rect rightRect = new Rect(rect.x + width, rect.y, width, rect.height);

            GUI.Label(leftRect, name);

            var outValue = GUI.TextField(rightRect, value.ToString());

            return outValue;
        }
        protected int DrawVariable(Rect rect, string name, int value)
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

        protected Person DrawVariable(Rect rect, string name, Person value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);

            Rect rightRect = new Rect(rect.x + rect.width - 18, rect.y, 18, rect.height);

            GUI.Label(leftRect, name);

            FriendPoint.PointRect = rightRect;

            return value;
        }

        protected void DrawVariable(ref Rect rect, ref bool toogleValue, FieldInfo fieldInfo, FiniteStateMachineData data)
        {

            if (fieldInfo.FieldType.IsArray)
            {
                int length;
                if (!(fieldInfo.GetValue(data) is Array value)) return;

                length = value.Length;

                toogleValue = GUI.Toggle(rect, toogleValue, fieldInfo.Name, EditorStyles.foldout);

                Rect lengthRect = rect;
                lengthRect.x = lengthRect.x + rect.width - 40;
                lengthRect.width = 40;
                string strLength = GUI.TextField(lengthRect, length.ToString());

                if (!int.TryParse(strLength, out int strLengthParse)) return;

                if (strLengthParse > length)
                {

                }
                else if (strLengthParse < length)
                {

                }
                if (toogleValue)
                {
                    // 显示元素
                    rect.x += 18;
                    rect.width -= 18f;

                    rect.y += 18;
                    GUI.Box(rect, "", "GroupBox");
                }

            }
            else
            {
                float width = rect.width * 0.5f;
                Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);
                Rect rightRect = new Rect(rect.x + width, rect.y, width, rect.height);

                GUI.Label(leftRect, fieldInfo.Name);

                var outValue = GUI.TextField(rightRect, fieldInfo.GetValue(data).ToString());

                fieldInfo.SetValue(data, outValue);
            }
        }
    }

    public class FSMNode : Node
    {
        private readonly NodeDataGraph graphData;

        public FSMNode(NodeDataGraph graphData, GUIStyle nodeStyle, GUIStyle inPointStyle, GUIStyle outPointStyle,
            Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
              : base(graphData.NodeRect.position, nodeStyle, inPointStyle, outPointStyle, onClickInPoint, onClickOutPoint, onRemoveNode)
        {
            this.graphData = graphData; 
        }

        public override void Draw()
        {
            var rect = NodeRect;

            GUI.Box(rect, GUIContent.none, NodeSkin.box);

            rect = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Label(rect, graphData.Type.Name, NodeSkin.customStyles[3]);



            rect = new Rect(rect.x + 2f, rect.y + rect.height + 5f, rect.width - 4f, 18f);

            for (int i = 0; i < graphData.FieldInfos.Length; i++)
            {
                DrawVariable(ref rect, ref graphData.Toogles[i], graphData.FieldInfos[i], graphData.FiniteStateMachineData);

                rect.y += 18f;
            }


            //graphData.FiniteStateMachineData.Name = DrawVariable(nameRect, "Name", graphData.FiniteStateMachineData.Name);

            //var ageRect = new Rect(rect.x + 2f, nameRect.y + nameRect.height, rect.width - 4f, 18f);
            //graphData.FiniteStateMachineData.AllStates = DrawVariable(ageRect, graphData., "AllStates", graphData.FiniteStateMachineData.AllStates);

            //var friendRect = new Rect(rect.x + 2f, ageRect.y + ageRect.height, rect.width - 4f, 18f);
            //Person.Friend = DrawVariable(friendRect, "Friend", Person.Friend);


            InPoint.Draw();
            OutPoint.Draw();
        }
    }

}