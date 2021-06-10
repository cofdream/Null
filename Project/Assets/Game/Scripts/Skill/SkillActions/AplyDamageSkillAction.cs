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
            Target.Value.UnitAttribute.Damage(DamageVariable.Value, Executor.Value);
        }

        public override void CloneVariables(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);
            Target = GetCloneInstance(allDependencies, Target);
            DamageVariable = GetCloneInstance(allDependencies, DamageVariable);
        }
    }
}