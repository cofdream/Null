using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class InputStateAction : StateAction
    {
        [HideInInspector] public UnitVariable UnitVariable;
        public MovementVariables MovementVariables;
        public bool RunButtonState;
        public BoolVariable JumpVariable;
        public override void OnUpdate()
        {
            RunButtonState = Input.GetAxis("Fire3") == 1;

            JumpVariable.Value = Input.GetAxis("Jump") == 1;

            MovementVariables.Horizontal = Input.GetAxis("Horizontal");
            MovementVariables.Vertical = Input.GetAxis("Vertical");

            float maxValue;
            if (RunButtonState)
                maxValue = 1;
            else
                maxValue = 0.25f;

            MovementVariables.MoveAmount = Mathf.Clamp(Mathf.Abs(MovementVariables.Horizontal) + Mathf.Abs(MovementVariables.Vertical), 0, maxValue);


            MovementVariables.MoveSpeed = UnitVariable.Value.UnitAttribute.MoveSpeed;
        }
    }
}