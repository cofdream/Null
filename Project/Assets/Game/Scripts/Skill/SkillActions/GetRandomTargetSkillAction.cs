using Game.Variable;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Skill
{
    public class GetRandomTargetSkillAction : SkillAction
    {
        public UnitVariable Executor;
        public UnitVariable Target;

        public override void CastSkill()
        {
            var allUnit = Test.TestManager.Instance.AllUnit.Where(
                (unit) =>
                {
                    return unit != Target.Value;
                }
                ).ToArray();

            if (allUnit.Length == 0)
            {
                return;
            }
            Target.Value = allUnit[Random.Range(0, allUnit.Length)];

        }
        public override void CloneVariables(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);
            Target = GetCloneInstance(allDependencies, Target);
        }
    }
}