using System;
using UnityEngine;

namespace DA.Node
{
    public enum ConnectionPointType
    {
        In,
        Out,
    }


    public class ConnectionPoint
    {
        public Rect Rect;
        public Node Node;
        public ConnectionPointType ConnectionPointType;
        public GUIStyle ConnectPointStyle;
        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, ConnectionPointType connectionPointType, GUIStyle style, Action<ConnectionPoint> onClickConnectionPoint)
        {
            //Rect = new Rect(0, 0, 18f, 18f);
            Node = node;
            ConnectionPointType = connectionPointType;
            ConnectPointStyle = style;
            OnClickConnectionPoint = onClickConnectionPoint;
        }

        public void Draw()
        {
            //Rect.y = NodeReferencePoint.PointRect.y + (NodeReferencePoint.PointRect.height * 0.5f) - Rect.height * 0.5f;

            if (GUI.Button(Rect, "", ConnectPointStyle))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }

    }
}