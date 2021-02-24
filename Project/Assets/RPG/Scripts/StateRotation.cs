using UnityEngine;

namespace RPG
{
    public class StateRotation : FSM.State
    {
        public enum RotationType
        {
            None = 0,
            Back,
            Left,
            Right,
        }

    }
}