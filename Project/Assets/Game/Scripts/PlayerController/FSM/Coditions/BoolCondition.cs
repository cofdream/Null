using Game.FSM;
using System;

namespace Game
{
    [Serializable]
    public class BoolCondition : Condition
    {
        public BoolVariable LeftBoolVariable;
        public BoolVariable RightBoolVariable;

        public override bool CheckStateChange()
        {
            return LeftBoolVariable.Value == RightBoolVariable.Value;
        }
    }
}
