using Game.Variable;
using System.Collections.Generic;
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
        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);
            Target = GetCloneInstance(allDependencies, Target);
            DamageVariable = GetCloneInstance(allDependencies, DamageVariable);
        }
    }
}