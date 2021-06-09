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

        public override void InstantiateDependencies(Dictionary<ScriptableObject, ScriptableObject> allDependencies)
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
            if (allDependencies.TryGetValue(DamageVariable, out scriptableObject))
            {
                DamageVariable = scriptableObject as IntVariable;
            }
            else
            {
                Debug.LogError("------------------------");
            }
        }
    }
}