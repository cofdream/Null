using Game.Variable;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Skill
{
    public class GetRandomTargetSkillAction : SkillAction
    {
        public UnitVariable Executor;
        public UnitVariable Target;

        public override void CastSkill()
        {
            Target.Value = Executor.Value;
        }
        public override void CloneVariables(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);
            Target = GetCloneInstance(allDependencies, Target);
        }
    }
}