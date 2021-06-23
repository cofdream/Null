﻿using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MovementVariables
    {
        public float Horizontal;
        public float Vertical;
        public float MoveAmount;
        public float MoveSpeed;
        public Vector3 MoveDirection;
    }
}