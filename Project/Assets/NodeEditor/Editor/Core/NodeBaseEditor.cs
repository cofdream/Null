using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class NodeBaseEditor : EditorWindow, IHasCustomMenu
    {
        //[MenuItem("NodeTool/OpenWindow")]
        //private static void OpemWindow()
        //{
        //    var window = GetWindow<NodeBaseEditor>();
        //    window.titleContent = new GUIContent("Node Based Editor"/*, AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.devilangel.iconkit/head/editor_head.png")*/);
        //    window.Show();
        //}

        public static void OpenNode(NullNamespace.NodeDataGraph finiteStateMachineDataGraph)
        {
            var window = GetWindow<NodeBaseEditor>();
            window.titleContent = new GUIContent("Node Based Editor"/*, AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.devilangel.iconkit/head/editor_head.png")*/);
            window.Show();

            window.InitGraph(finiteStateMachineDataGraph);
        }

        public void AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Reset"), false, MenuItem_Refresh);
        }
        private void MenuItem_Refresh()
        {
            if (nodeList != null) nodeList.Clear();
            else nodeList = new List<Node>();

            if (connectionList != null) connectionList.Clear();
            else connectionList = new List<Connection>();

            InitStyles();
        }

        private List<Node> nodeList;
        private List<Connection> connectionList;

        private GUIStyle nodeStyle;
        private GUIStyle inPointStyle;
        private GUIStyle outPointStyle;
        private Texture2D gridBackgroundTexture;

        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;

        private Vector2 offset;
        private Vector2 drag;

        private float gridSpacing = 15f;

        private const float minGridSpacing = 9f;
        private const float maxGridSpacing = 30f;
        private readonly Color smallLineColor = new Color(34f / 255, 34f / 255, 34f / 255, 255);
        private readonly Color bigLineColor = new Color(24f / 255, 24f / 255, 24f / 255, 255);

        private void Awake()
        {
            InitStyles();
            LoadData();
        }

        private void OnGUI()
        {
            if (nodeList == null || connectionList == null) Close();

            var _event = UnityEngine.Event.current;

            GUI.DrawTexture(new Rect(0, 0, position.width, position.height), gridBackgroundTexture, ScaleMode.StretchToFill);

            DrawGrids();

            DrawConnectionLine(_event.mousePosition);

            DrawNodes();
            DrawConnections();

            ProcessNodeEvent(_event);

            ProcessEvents(_event);

            if (GUI.changed)
                base.Repaint();
        }

        private void InitGraph(NullNamespace.NodeDataGraph finiteStateMachineDataGraph)
        {
            if (nodeList.Count == 0)
            {
                Node node = new FSMNode(finiteStateMachineDataGraph, nodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
                nodeList.Add(node);
            }
        }
        private void InitStyles()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(3, 3, 10, 10);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);

            gridBackgroundTexture = new Texture2D(1, 1);
            gridBackgroundTexture.SetPixel(0, 0, new Color(42f / 255, 42f / 255, 42f / 255, 255));
            gridBackgroundTexture.Apply();
        }

        private void LoadData()
        {
            if (nodeList == null)
                nodeList = new List<Node>();
            if (connectionList == null)
                connectionList = new List<Connection>();
        }

        private void DrawGrids()
        {
            Handles.BeginGUI();
            Color oldColor = Handles.color;

            offset += drag;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            Handles.color = smallLineColor;
            DrawGrid(gridSpacing, newOffset);

            Handles.color = bigLineColor;
            DrawGrid(gridSpacing * 10, newOffset);

            Handles.color = oldColor;
            Handles.EndGUI();
        }
        private void DrawGrid(float gridSpacing, Vector3 newOffset)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            for (int i = 0; i < widthDivs; i++)
            {
                var p1 = new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset;
                var p2 = new Vector3(p1.x, position.height, 0) + newOffset;
                p1.x = (int)p1.x + 0.2f;
                p2.x = p1.x;

                Handles.DrawLine(p1, p2);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                var p1 = new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset;
                var p2 = new Vector3(position.width, p1.y, 0) + newOffset;
                p1.y = (int)p1.y + 0.2f;
                p2.y = p1.y;

                Handles.DrawLine(p1, p2);
            }
        }

        [System.Obsolete]
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Color oldColor = Handles.color;
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = oldColor;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            foreach (var node in nodeList)
            {
                node.Draw();
            }

        }
        private void DrawConnections()
        {
            //foreach (var connection in connectionList)
            //{
            //    connection.Draw();
            //}
            for (int i = connectionList.Count - 1; i > -1; i--)
            {
                connectionList[i].Draw();
            }
        }

        private void DrawConnectionLine(Vector2 endPosition)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.Rect.center,
                    endPosition,
                    selectedInPoint.Rect.center + Vector2.left * 50f,
                    endPosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.Rect.center,
                    endPosition,
                    selectedOutPoint.Rect.center - Vector2.left * 50f,
                    endPosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        private void ProcessEvents(UnityEngine.Event _event)
        {
            drag = Vector2.zero;

            switch (_event.type)
            {
                case EventType.MouseDown:
                    if (_event.button == 1)
                    {
                        ProcessContextMenu(_event.mousePosition);
                    }
                    break;
                case EventType.MouseDrag:
                    if (_event.button == 0)
                    {
                        OnDrag(_event.delta);
                    }
                    break;
                case EventType.ScrollWheel:
                    OnGridZoom(_event);
                    break;
            }
        }

        private void ProcessNodeEvent(UnityEngine.Event _event)
        {
            for (int i = nodeList.Count - 1; i > -1; i--)
            {
                nodeList[i].ProcessEvents(_event);
            }
        }


        private void ProcessContextMenu(Vector2 mousPosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            genericMenu.AddItem(new GUIContent("Add Person Node"), false, () =>
            {
                Node node = new Node(mousPosition, nodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
                nodeList.Add(node);
            });

            genericMenu.ShowAsContext();
        }

        private void OnDrag(Vector2 delta)
        {
            drag = delta;
            foreach (var node in nodeList)
            {
                node.Drag(delta);
            }
            GUI.changed = true;
        }


        private void OnGridZoom(UnityEngine.Event _event)
        {
            gridSpacing = Mathf.Clamp(gridSpacing - (_event.delta.y * 0.1f), minGridSpacing, maxGridSpacing);

            GUI.changed = true;
        }


        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.Node != selectedInPoint.Node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.Node != selectedInPoint.Node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickRemoveConnection(Connection connection)
        {
            connectionList.Remove(connection);
        }

        private void CreateConnection()
        {
            connectionList.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }

        private void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }

        private void OnClickRemoveNode(Node node)
        {
            if (connectionList != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < connectionList.Count; i++)
                {
                    if (connectionList[i].InPoint == node.InPoint || connectionList[i].OutPoint == node.OutPoint)
                    {
                        connectionsToRemove.Add(connectionList[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    connectionList.Remove(connectionsToRemove[i]);
                }
            }

            nodeList.Remove(node);
        }
    }
}