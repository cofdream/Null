using Game.Variable;
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
    }
}