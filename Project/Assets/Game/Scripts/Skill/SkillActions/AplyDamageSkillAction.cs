using Game.Variable;
using System.Collections.Generic;
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
            if (Executor.Value == null)
            {
                return;
            }
            Target.Value.UnitAttribute.Damage(DamageVariable.Value, Executor.Value);
        }

        protected override void CloneDependencies(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);
            Target = GetCloneInstance(allDependencies, Target);
            DamageVariable = GetCloneInstance(allDependencies, DamageVariable);
        }
    }
}