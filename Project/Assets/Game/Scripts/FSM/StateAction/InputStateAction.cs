using DA.Core.FSM;
using UnityEngine;

namespace Game
{
    public class InputStateAction : StateAction
    {
        public Unit Unit;
        public MovementVarible MovementVarible;
        public override void OnUpdate()
        {
            MovementVarible.Horizontal = Input.GetAxis("Horizontal");
            MovementVarible.Vertical = Input.GetAxis("Vertical");

            MovementVarible.MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementVarible.Horizontal) + Mathf.Abs(MovementVarible.Vertical));

            MovementVarible.MoveSpeed = Unit.UnitAttribute.MoveSpeed;
        }
    }
}