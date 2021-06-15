using Game.FSM;
using Game.Variable;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class InputStateAction : StateAction
    {
        [HideInInspector] public UnitVariable UnitVariable;
        [SerializeReference] public MovementVariables MovementVariables;
        public override void OnUpdate()
        {
            MovementVariables.Horizontal = Input.GetAxis("Horizontal");
            MovementVariables.Vertical = Input.GetAxis("Vertical");

            MovementVariables.MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementVariables.Horizontal) + Mathf.Abs(MovementVariables.Vertical));

            MovementVariables.MoveSpeed = UnitVariable.Value.UnitAttribute.MoveSpeed;
        }
    }
}