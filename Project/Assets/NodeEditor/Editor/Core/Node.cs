using Game.FSM;
using NullNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class Node
    {
        public Rect NodeRect;
        public bool IsDragged;
        private bool isResizeHorizontal;
        private bool isResizeVertical;

        public GUIStyle NodeGUIStyle;

        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;

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

            OnRemoveNode = onRemoveNode;
        }

        public void Drag(Vector2 delta)
        {
            NodeRect.position += delta;
        }

        public virtual void Draw()
        {
            GUI.Box(NodeRect, "");
        }

        public void ProcessEvents(UnityEngine.Event _event)
        {
            var mousePosition = _event.mousePosition;


            float angleSize = 4;
            float frameSize = 2;

            //Rect leftFrameRect = new Rect(NodeRect.x, NodeRect.y - angleSize, 1, NodeRect.height - angleSize * 2);
            Rect rightFrameRect = new Rect(NodeRect.x + NodeRect.width - frameSize, NodeRect.y - angleSize, frameSize * 2, NodeRect.height - angleSize * 2);
            //Rect upFrameRect = new Rect(NodeRect.x - angleSize, NodeRect.y, NodeRect.width - angleSize * 2, 1);
            Rect downFrameRect = new Rect(NodeRect.x - angleSize, NodeRect.y + NodeRect.height - frameSize, NodeRect.width - angleSize * 2, frameSize * 2);

            //Rect leftUpAngleRect = new Rect(NodeRect.x, NodeRect.y, angleSize, angleSize);
            //Rect rightUpAngleRect = new Rect(NodeRect.x + NodeRect.width - angleSize, NodeRect.y, angleSize, angleSize);
            //Rect leftDownAngleRect = new Rect(NodeRect.x, NodeRect.y - NodeRect.height - angleSize, angleSize, angleSize);
            Rect rightDownAngleRect = new Rect(NodeRect.x + NodeRect.width - angleSize, NodeRect.y + NodeRect.height - angleSize, angleSize * 2, angleSize * 2);


            //EditorGUIUtility.AddCursorRect(leftFrameRect, MouseCursor.ResizeHorizontal);
            EditorGUIUtility.AddCursorRect(rightFrameRect, MouseCursor.ResizeHorizontal);
            //EditorGUIUtility.AddCursorRect(upFrameRect, MouseCursor.ResizeVertical);
            EditorGUIUtility.AddCursorRect(downFrameRect, MouseCursor.ResizeVertical);

            //EditorGUIUtility.AddCursorRect(leftUpAngleRect, MouseCursor.ResizeUpLeft);
            //EditorGUIUtility.AddCursorRect(rightUpAngleRect, MouseCursor.ResizeUpRight);
            //EditorGUIUtility.AddCursorRect(leftDownAngleRect, MouseCursor.ResizeUpRight);
            EditorGUIUtility.AddCursorRect(rightDownAngleRect, MouseCursor.ResizeUpLeft);

            switch (_event.type)
            {
                case EventType.MouseDown:
                    if (_event.button == 0)
                    {

                        if (rightFrameRect.Contains(mousePosition))
                        {
                            isResizeHorizontal = true;
                        }
                        else if (downFrameRect.Contains(mousePosition))
                        {
                            isResizeVertical = true;
                        }
                        else if (rightDownAngleRect.Contains(mousePosition))
                        {
                            isResizeHorizontal = true;
                            isResizeVertical = true;
                        }
                        else
                        {
                            if (NodeRect.Contains(mousePosition))
                            {
                                IsDragged = true;
                            }
                        }
                    }

                    if (_event.button == 1 && NodeRect.Contains(mousePosition))
                    {
                        ProcessContextMenu();
                        _event.Use();
                        return;
                    }

                    break;
                case EventType.MouseUp:

                    isResizeHorizontal = false;
                    isResizeVertical = false;
                    IsDragged = false;

                    break;
                case EventType.MouseDrag:
                    if (_event.button == 0)
                    {
                        if (isResizeHorizontal)
                        {
                            NodeRect.width += _event.delta.x;
                        }
                        if (isResizeVertical)
                        {
                            NodeRect.height += _event.delta.y;
                        }

                        if (isResizeHorizontal || isResizeVertical)
                        {
                            _event.Use();
                        }
                        else
                        {
                            if (IsDragged)
                            {
                                Drag(_event.delta);
                                _event.Use();
                            }
                        }
                    }
                    break;
            }
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

            return value;
        }

        protected StateData DrawVariable(Rect rect, string name, StateData value)
        {
            float width = rect.width * 0.5f;
            Rect leftRect = new Rect(rect.x, rect.y, width, rect.height);

            Rect rightRect = new Rect(rect.x + rect.width - 18, rect.y, 18, rect.height);

            GUI.Label(leftRect, name);

            GUI.Box(rightRect, string.Empty);

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

        public ConnectionPoint[] InConnectionPoints;
        public ConnectionPoint[] OutConnectionPoints;

        //public GUIContent iconToolbarPlus = EditorGUIUtility.TrIconContent("Toolbar Plus", "Add to list");
        //public GUIContent iconToolbarPlusMore = EditorGUIUtility.TrIconContent("Toolbar Plus More", "Choose to add to list");

        //public readonly GUIStyle draggingHandle = "RL DragHandle";
        //public readonly GUIStyle headerBackground = "RL Header";
        //private readonly GUIStyle emptyHeaderBackground = "RL Empty Header";
        //public readonly GUIStyle footerBackground = "RL Footer";
        //public readonly GUIStyle boxBackground = "RL Background";
        //public readonly GUIStyle preButton = "RL FooterButton";
        //public readonly GUIStyle elementBackground = "RL Element";

        public FSMNode(NodeDataGraph graphData, GUIStyle nodeStyle, GUIStyle inPointStyle, GUIStyle outPointStyle,
            Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode)
              : base(graphData.NodeRect.position, nodeStyle, inPointStyle, outPointStyle, onClickInPoint, onClickOutPoint, onRemoveNode)
        {
            this.graphData = graphData;

            InConnectionPoints = new ConnectionPoint[]
            {
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
            };

            OutConnectionPoints = new ConnectionPoint[]
            {
                 new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
                 new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
                 new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
            };
        }


        int index;
        public override void Draw()
        {

            var rect = NodeRect;

            GUI.Box(rect, GUIContent.none, NodeSkin.box);

            rect = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Label(rect, graphData.DataType.Name, NodeSkin.customStyles[3]);


            rect = new Rect(rect.x + 2f, rect.y + rect.height + 5f, rect.width - 4f, 18f);
            graphData.FiniteStateMachineData.Name = DrawVariable(rect, "Name", graphData.FiniteStateMachineData.Name);
            rect.y += 18f;

            graphData.Toogle = GUI.Toggle(rect, graphData.Toogle, "AllState", EditorStyles.foldout);

            int length = graphData.FiniteStateMachineData.AllStates.Length;

            Rect lengthRect = rect;
            lengthRect.x = lengthRect.x + rect.width - 40;
            lengthRect.width = 40;
            string strLength = GUI.TextField(lengthRect, length.ToString());

            if (int.TryParse(strLength, out int strLengthParse))
            {
                if (strLengthParse > length)
                {

                }
                else if (strLengthParse < length)
                {

                }
            }

            if (graphData.Toogle)
            {
                // 显示元素
                rect.x += 18;
                rect.width -= 18f;

                rect.y += 18;
                for (int i = 0; i < length; i++)
                {
                    var stateData = graphData.FiniteStateMachineData.AllStates[i];

                    DrawVariable(rect, stateData.StateName, stateData);

                    var pointRect = new Rect(rect.x + rect.width - 18, rect.y, 18, 18);
                    InConnectionPoints[i].Rect = pointRect;
                    InConnectionPoints[i].Draw();

                    rect.y += 18;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    InConnectionPoints[i].Rect = lengthRect;
                }
            }
        }
    }
}