using Game.Variable;
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