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
        public ConnectionPointType ConnectionPointType;
        public NodeReferencePoint NodeReferencePoint;
        public GUIStyle ConnectPointStyle;
        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(NodeReferencePoint nodeReferencePoint, ConnectionPointType connectionPointType, GUIStyle style, Action<ConnectionPoint> onClickConnectionPoint)
        {
            Rect = new Rect(0, 0, 18f, 18f);
            NodeReferencePoint = nodeReferencePoint;
            ConnectionPointType = connectionPointType;
            ConnectPointStyle = style;
            OnClickConnectionPoint = onClickConnectionPoint;
        }

        public void Draw()
        {
            Rect.y = NodeReferencePoint.PointRect.y + (NodeReferencePoint.PointRect.height * 0.5f) - Rect.height * 0.5f;


            switch (ConnectionPointType)
            {
                case ConnectionPointType.In:
                    Rect.x = NodeReferencePoint.PointRect.x;
                    break;

                case ConnectionPointType.Out:
                    Rect.x = NodeReferencePoint.PointRect.x;
                    break;
            }

            if (GUI.Button(Rect, ""/*, ConnectPointStyle*/))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }

    }
}