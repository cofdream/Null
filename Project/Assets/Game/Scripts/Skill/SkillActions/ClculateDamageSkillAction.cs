using Game.Variable;
using UnityEngine;

namespace Game.Skill
{
    public class ClculateDamageSkillAction : SkillAction
    {
        public UnitVariable Executor;
        public UnitVariable Target;

        public DamageType DamageType;
        public AdditionType AdditionType;

        public int BaseDamage;
        public IntVariable DamageVariable;

        public override void CastSkill()
        {
            DamageVariable.Value = BaseDamage;
        }
    }
}