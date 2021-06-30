using Game.FSM;
using System;

namespace Game
{

    [Serializable]
    public class BoolCondition : ConditionOld
    {
        [Serializable]
        public enum BooleanType : byte
        {
            True = 0,
            False,
        }

        public BooleanType BoolType;
        public BoolVariable BoolVariable;

        public override bool CheckStateChange()
        {
            switch (BoolType)
            {
                default:
                case BooleanType.True:
                    return BoolVariable.Value;
                case BooleanType.False:
                    return !BoolVariable.Value;
            }
        }
    }
}
