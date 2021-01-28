using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class NodeBaseEditor : EditorWindow
    {
        [MenuItem("NodeTool/OpenWindow")]
        private static void OpemWindow()
        {
            var window = GetWindow<NodeBaseEditor>();
            window.titleContent = new GUIContent("Node Based Editor", AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.devilangel.iconkit/head/editor_head.png"));
            window.Show();
        }

        private List<Node> nodeList;
        private List<Connection> connectionList;

        private GUIStyle nodeStyle;
        private GUIStyle inPointStyle;
        private GUIStyle outPointStyle;

        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;

        private Vector2 offset;
        private Vector2 drag;

        private void Awake()
        {
            nodeList = new List<Node>();
            connectionList = new List<Connection>();
        }

        private void OnEnable()
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
        }

        private void OnGUI()
        {
            var _event = UnityEngine.Event.current;

            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(_event);

            ProcessNodeEvent(_event);
            processEvents(_event);

            if (GUI.changed)
            {
                base.Repaint();
                Debug.Log("Repaint.");
            }
        }
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
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

            Handles.color = Color.white;
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
            foreach (var connection in connectionList)
            {
                connection.Draw();
            }
        }

        private void DrawConnectionLine(UnityEngine.Event _event)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.Rect.center,
                    _event.mousePosition,
                    selectedInPoint.Rect.center + Vector2.left * 50f,
                    _event.mousePosition - Vector2.left * 50f,
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
                    _event.mousePosition,
                    selectedOutPoint.Rect.center - Vector2.left * 50f,
                    _event.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        private void processEvents(UnityEngine.Event _event)
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
                default:
                    break;
            }
        }

        private void ProcessNodeEvent(UnityEngine.Event _event)
        {
            for (int i = nodeList.Count - 1; i > -1; i--)
            {
                bool guiChange = nodeList[i].ProcessEvents(_event);
                if (guiChange)
                {
                    if (!GUI.changed) GUI.changed = true;
                }
            }
        }

        private void ProcessContextMenu(Vector2 mousPosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            nodeCreatePostion = mousPosition;
            genericMenu.AddItem(new GUIContent("Add Node"), false, OnClickAddNode);
            genericMenu.ShowAsContext();
        }
        private static Vector2 nodeCreatePostion;
        private void OnClickAddNode()
        {
            nodeList.Add(new Node(nodeCreatePostion, 200, 50, nodeStyle,
                inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint,
                OnClickRemoveNode));
        }
        private void OnDrag(Vector2 delta)
        {
            drag = delta;
            foreach (var node in nodeList)
            {
                node.Drag(delta);
            }
            if (!GUI.changed) GUI.changed = true;
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

                connectionsToRemove = null;
            }

            nodeList.Remove(node);
        }
    }
}