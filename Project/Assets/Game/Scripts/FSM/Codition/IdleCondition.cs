using DA.Core.FSM;

namespace Game
{
    public class IdleCondition : Condition
    {
        public MovementVariables MovementVarible;
        public override bool CheckStateChange()
        {
            return MovementVarible.MoveAmount == 0;
        }
    }
}