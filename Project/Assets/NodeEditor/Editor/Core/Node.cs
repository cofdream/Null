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
    public abstract class Node
    {
        public bool IsDragged;
        private bool isResizeHorizontal;
        private bool isResizeVertical;

        public Action<Node> OnRemoveNode;

        protected static GUISkin NodeSkin;
        static Node()
        {
            NodeSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/NodeEditor/PersonGUISkin.guiskin");
        }

        public Node(Action<Node> onRemoveNode)
        {
            OnRemoveNode = onRemoveNode;
        }

        public virtual Rect GetRect()
        {
            return Rect.zero;
        }
        public virtual void Drag(Vector2 delta)
        {

        }
        public virtual void Resize(Vector2 size)
        {

        }

        public virtual void Draw()
        {
            GUI.Box(GetRect(), "Node");
        }

        public virtual void ProcessEvents(UnityEngine.Event _event)
        {
            var rect = GetRect();

            var mousePosition = _event.mousePosition;

            float angleSize = 4;
            float frameSize = 2;

            //Rect leftFrameRect = new Rect(rect.x, rect.y - angleSize, 1, rect.height - angleSize * 2);
            Rect rightFrameRect = new Rect(rect.x + rect.width - frameSize, rect.y - angleSize, frameSize * 2, rect.height - angleSize * 2);
            //Rect upFrameRect = new Rect(rect.x - angleSize, rect.y, rect.width - angleSize * 2, 1);
            Rect downFrameRect = new Rect(rect.x - angleSize, rect.y + rect.height - frameSize, rect.width - angleSize * 2, frameSize * 2);

            //Rect leftUpAngleRect = new Rect(rect.x, rect.y, angleSize, angleSize);
            //Rect rightUpAngleRect = new Rect(rect.x + rect.width - angleSize, rect.y, angleSize, angleSize);
            //Rect leftDownAngleRect = new Rect(rect.x, rect.y - rect.height - angleSize, angleSize, angleSize);
            Rect rightDownAngleRect = new Rect(rect.x + rect.width - angleSize, rect.y + rect.height - angleSize, angleSize * 2, angleSize * 2);


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
                            if (rect.Contains(mousePosition))
                            {
                                IsDragged = true;
                            }
                        }
                    }

                    if (_event.button == 1 && rect.Contains(mousePosition))
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
                            Resize(new Vector2(_event.delta.x, 0));
                        }
                        if (isResizeVertical)
                        {
                            Resize(new Vector2(0, _event.delta.y));
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
            AddMenu(genericMenu);
            genericMenu.ShowAsContext();
        }
        protected virtual void AddMenu(GenericMenu menu)
        {

        }
        protected virtual void OnClickRemoveNode()
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
        private readonly FiniteStateMachineDataGraph graph;

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

        private Action<Node> onAddNode;
        public FSMNode(FiniteStateMachineDataGraph graphData, Action<ConnectionPoint> onClickInPoint, Action<Node> onAddNode, Action<Node> onRemoveNode) : base(onRemoveNode)
        {
            this.graph = graphData;

            this.onAddNode = onAddNode;

            InConnectionPoints = new ConnectionPoint[]
            {
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
                 new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
            };
        }

        public override Rect GetRect()
        {
            return graph.NodeRect;
        }
        public override void Drag(Vector2 delta)
        {
            graph.NodeRect.position += delta;
        }
        public override void Resize(Vector2 size)
        {
            graph.NodeRect.size += size;
        }

        public override void Draw()
        {
            var rect = graph.NodeRect;

            GUI.Box(rect, GUIContent.none, NodeSkin.box);

            rect = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Label(rect, graph.GetType().Name, NodeSkin.customStyles[3]);


            rect = new Rect(rect.x + 2f, rect.y + rect.height + 5f, rect.width - 4f, 18f);
            graph.FiniteStateMachineData.Name = DrawVariable(rect, "Name", graph.FiniteStateMachineData.Name);
            rect.y += 18f;

            graph.Toogle = GUI.Toggle(rect, graph.Toogle, "AllState", EditorStyles.foldout);

            int length = graph.FiniteStateMachineData.StateDatas.Length;

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

            if (graph.Toogle)
            {
                // 显示元素
                rect.x += 18;
                rect.width -= 18f;

                rect.y += 18;
                for (int i = 0; i < length; i++)
                {
                    var stateData = graph.FiniteStateMachineData.StateDatas[i];

                    DrawVariable(rect, stateData.StateName, stateData);

                    var pointRect = new Rect(rect.x + rect.width - 18, rect.y, 18, 18);
                    InConnectionPoints[i].PointRect = pointRect;
                    InConnectionPoints[i].Draw();

                    rect.y += 18;
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    InConnectionPoints[i].PointRect = lengthRect;
                }
            }
        }

        protected override void AddMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Add State"), false, graph.AddState);
        }
    }

    public class StateNode : Node
    {
        public StateDataGraph graph;

        public ConnectionPoint OutConnectionPoint;

        public StateNode(StateDataGraph stateGraph, Action<ConnectionPoint> onClickInPoint, Action<ConnectionPoint> onClickOutPoint, Action<Node> onRemoveNode) : base(onRemoveNode)
        {
            graph = stateGraph;

            OutConnectionPoint = new ConnectionPoint(this, ConnectionPointType.Out, NodeSkin.toggle, onClickOutPoint);
            //OutConnectionPoint.Rect.position = NodeRect.position;
            //OutConnectionPoint.Rect.size = new Vector2(NodeRect.width - 4, NodeRect.height - 4);

            //InConnectionPoints = new ConnectionPoint[]
            //{
            //     new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
            //     new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
            //     new ConnectionPoint(this,ConnectionPointType.In,NodeSkin.toggle,onClickInPoint),
            //};

            //OutConnectionPoints = new ConnectionPoint[]
            //{
            //     new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
            //     new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
            //     new ConnectionPoint(this,ConnectionPointType.Out,NodeSkin.toggle,onClickOutPoint),
            //};
        }

        public override Rect GetRect()
        {
            return graph.NodeRect;
        }
        public override void Drag(Vector2 delta)
        {
            graph.NodeRect.position += delta;
        }
        public override void Resize(Vector2 size)
        {
            graph.NodeRect.size += size;
        }

        public override void Draw()
        {
            var rect = graph.NodeRect;

            GUI.Box(rect, GUIContent.none, NodeSkin.box);

            rect = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Label(rect, graph.GetType().Name, NodeSkin.customStyles[3]);
        }
    }
}