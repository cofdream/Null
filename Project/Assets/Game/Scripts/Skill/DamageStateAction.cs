using DA.Core.FSM;
using UnityEngine;

namespace Game.Skill
{
    public class DamageStateAction : StateAction
    {
        public Unit Executor;
        public Unit Target;
        public int damage;
        public override void OnEnter()
        {
            Target.UnitAttribute.Damage(damage, Target);
        }
    }
}