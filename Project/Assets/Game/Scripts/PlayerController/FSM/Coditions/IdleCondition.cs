using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class IdleCondition : Condition
    {
        [SerializeReference] public MovementVariables MovementVarible;
        public override bool CheckStateChange()
        {
            return MovementVarible.MoveAmount == 0;
        }
    }
}