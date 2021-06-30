using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class State : ScriptableObject
    {
        public virtual void OnEnter(PlayerController playerController)
        {

        }
        public virtual void OnUpdate(PlayerController playerController)
        {

        }
        public virtual void OnFixedUpdate(PlayerController playerController)
        {

        }
        public virtual void OnExit(PlayerController playerController)
        {

        }
    }
}