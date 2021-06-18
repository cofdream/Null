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
        public Node Node;
        public GUIStyle ConnectPointStyle;
        public Action<ConnectionPoint> OnClickConnectionPoint;

        public ConnectionPoint(Node node, ConnectionPointType connectionPointType, GUIStyle style, Action<ConnectionPoint> onClickConnectionPoint)
        {
            Rect = new Rect(0, 0, 10f, 20f);
            Node = node;
            ConnectionPointType = connectionPointType;
            ConnectPointStyle = style;
            OnClickConnectionPoint = onClickConnectionPoint;
        }

        public void Draw()
        {
            Rect.y = Node.NodeRect.y + (Node.NodeRect.height * 0.5f) - Rect.height * 0.5f;


            switch (ConnectionPointType)
            {
                case ConnectionPointType.In:
                    Rect.x = Node.NodeRect.x - Rect.width + 8f;
                    break;

                case ConnectionPointType.Out:
                    Rect.x = Node.NodeRect.x + Node.NodeRect.width - 8f;
                    break;
            }

            if (GUI.Button(Rect, "", ConnectPointStyle))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }

    }
}