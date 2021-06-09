using DA.Core.FSM;
using Game.Variable;
using UnityEngine;

namespace Game.Skill
{
    public class AplyDamageSkillAction : SkillAction
    {
        public UnitVariable Executor;
        public UnitVariable Target;

        public IntVariable DamageVariable;

        public override void CastSkill()
        {
            Target.Value.UnitAttribute.Damage(DamageVariable.Value, Executor.Value);
        }
    }
}