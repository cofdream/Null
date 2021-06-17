using Game.FSM;
using System;

namespace Game
{
    [Serializable]
    public enum IntCompareType : byte
    {
        Greater,
        Less,
        Equals,
        GreaterEquals,
        LessEquals,
        NotEqual,
    }

    [Serializable]
    public class IntCondition : Condition
    {
        public IntCompareType CompareType;
        public IntVariable LeftVariable;
        public IntVariable RightVariable;
        public override bool CheckStateChange()
        {
            switch (CompareType)
            {
                case IntCompareType.Greater:       return LeftVariable.Value >  RightVariable.Value;
                case IntCompareType.Less:          return LeftVariable.Value <  RightVariable.Value;
                case IntCompareType.GreaterEquals: return LeftVariable.Value >= RightVariable.Value;
                case IntCompareType.LessEquals:    return LeftVariable.Value <= RightVariable.Value;
                case IntCompareType.Equals:        return LeftVariable.Value == RightVariable.Value;
                case IntCompareType.NotEqual:      return LeftVariable.Value != RightVariable.Value;   
            }
            return false;
        }
    }
}
