using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class MoveCondition : Condition
    {
        [SerializeReference] public MovementVariables MovementVariable;
        public override bool CheckStateChange()
        {
            return MovementVariable.MoveAmount > 0;
        }
    }
}