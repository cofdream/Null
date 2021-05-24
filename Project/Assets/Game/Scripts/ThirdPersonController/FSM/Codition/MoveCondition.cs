using DA.Core.FSM;

namespace Game
{
    public class MoveCondition : Condition
    {
        public MovementVarible MovementVarible;
        public override bool CheckStateChange()
        {
            return MovementVarible.MoveAmount > 0;
        }
    }
}