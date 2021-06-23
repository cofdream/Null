﻿
using System;

namespace Game.FSM
{
    [Serializable]
    public class StateAction
    {
        public bool Active = true;

        public virtual void Init()
        {

        }

        public virtual void OnEnter()
        {

        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnFixedUpdate()
        {

        }
        public virtual void OnExit()
        {

        }
    }
}