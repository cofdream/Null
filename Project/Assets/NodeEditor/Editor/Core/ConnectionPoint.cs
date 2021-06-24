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
        public Rect PointRect;
        public Node Node;
        public ConnectionPointType ConnectionPointType;
        public GUIStyle ConnectPointStyle;
        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, ConnectionPointType connectionPointType, GUIStyle style, Action<ConnectionPoint> onClickConnectionPoint)
        {
            Node = node;
            ConnectionPointType = connectionPointType;
            ConnectPointStyle = style;
            OnClickConnectionPoint = onClickConnectionPoint;
        }
        
        public void Draw()
        {
            if (GUI.Button(PointRect, "", ConnectPointStyle))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }
    }
}