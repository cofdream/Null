using Game.Variable;
using UnityEngine;
using System.Linq;

namespace Game
{
    [System.Serializable]
    public class GetRandomTargetSkillAction : SkillAction
    {
        [HideInInspector] public UnitVariable Executor;
        [HideInInspector] public UnitVariable Target;

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
                Debug.Log("找不到目标");
                return;
            }
            Target.Value = allUnit[Random.Range(0, allUnit.Length)];

        }

    }
}