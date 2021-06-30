using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class InputStateAction : StateActionOld
    {
        public MovementVariable MovementVariables;
        public BoolVariable JumpVariable;
        public BoolVariable IsMovement;
        public BoolVariable IsRun;

        public override void OnUpdate()
        {
            IsRun.Value = Input.GetAxis("Fire3") == 1;

            JumpVariable.Value = Input.GetButtonDown("Jump");


            MovementVariables.Horizontal = Input.GetAxis("Horizontal");
            MovementVariables.Vertical = Input.GetAxis("Vertical");

            float maxValue;
            if (IsRun.Value)
                maxValue = 1;
            else
                maxValue = 0.25f;

            var moveAmount = Mathf.Clamp(Mathf.Abs(MovementVariables.Horizontal) + Mathf.Abs(MovementVariables.Vertical), 0, maxValue);
            MovementVariables.MoveAmount = moveAmount;

            IsMovement.Value = moveAmount > 0.1f;
        }
    }
}