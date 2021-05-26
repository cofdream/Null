using DA.Core.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class InputStateAction : StateAction
    {
        public Unit Unit;
        public MovementVariables MovementVariables;
        public override void OnUpdate()
        {
            MovementVariables.Horizontal = Input.GetAxis("Horizontal");
            MovementVariables.Vertical = Input.GetAxis("Vertical");

            MovementVariables.MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementVariables.Horizontal) + Mathf.Abs(MovementVariables.Vertical));

            MovementVariables.MoveSpeed = Unit.UnitAttribute.MoveSpeed;
        }
    }
}