using Game.FSM;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class IdleCondition : ConditionOld
    {
        [SerializeReference] public MovementVariable MovementVarible;
        public override bool CheckStateChange()
        {
            return MovementVarible.MoveAmount == 0;
        }
    }
}