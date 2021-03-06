﻿using System;
using UnityEditor;
using UnityEngine;

namespace DA.Node
{
    public class Connection
    {
        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;
        public Action<Connection> OnClickRemoveConnection;

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> onClickRemoveConnection)
        {
            InPoint = inPoint;
            OutPoint = outPoint;
            OnClickRemoveConnection = onClickRemoveConnection;
        }

        public void Draw()
        {
            Handles.DrawBezier(
                InPoint.PointRect.center,
                OutPoint.PointRect.center,
                InPoint.PointRect.center + Vector2.left * 50f,
                OutPoint.PointRect.center - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );
            

            if (Handles.Button((InPoint.PointRect.center + OutPoint.PointRect.center) * 0.5f, Quaternion.identity, 8, 16, Handles.SphereHandleCap))
            {
                OnClickRemoveConnection?.Invoke(this);
            }
        }
    }
}