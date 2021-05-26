using DA.Core.FSM;

namespace Game
{
    public class MoveCondition : Condition
    {
        public MovementVariables MovementVariables;
        public override bool CheckStateChange()
        {
            return MovementVariables.MoveAmount > 0;
        }
    }
}