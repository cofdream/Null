using Game.FSM;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
	public class FloatCondition : Condition
	{
        public bool IsGreater;
        public FloatVariable LeftVariable;
        public FloatVariable RightVariable;
        public override bool CheckStateChange()
        {
            if (IsGreater)
                return LeftVariable.Value > RightVariable.Value;
            else
                return LeftVariable.Value < RightVariable.Value;
        }
    }
}