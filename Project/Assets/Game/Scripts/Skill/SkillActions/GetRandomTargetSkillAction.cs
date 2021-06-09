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
        public override void InstantiateDependencies(System.Collections.Generic.Dictionary<ScriptableObject, ScriptableObject> allDependencies)
        {
            if (allDependencies.TryGetValue(Executor, out ScriptableObject scriptableObject))
            {
                Executor = scriptableObject as UnitVariable;
            }
            else
            {
                Debug.LogError("------------------------");
            }

            if (allDependencies.TryGetValue(Target, out scriptableObject))
            {
                Target = scriptableObject as UnitVariable;
            }
            else
            {
                Debug.LogError("------------------------");
            }
        }
    }
}