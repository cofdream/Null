using UnityEngine;
using System.Linq;

namespace Game
{
    [System.Serializable]
    public class GetRandomTargetSkillAction : SkillAction
    {
        [HideInInspector] public UnitVariable Executor;
        [HideInInspector] public UnitVariable Target;

        public UnitAttribute ExecutorAttribute;
        public UnitAttribute TargetAttribute;

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
            }
            else
                Target.Value = allUnit[Random.Range(0, allUnit.Length)];
        }
    }
}