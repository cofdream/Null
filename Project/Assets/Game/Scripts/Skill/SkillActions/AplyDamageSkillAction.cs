using Game.Variable;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AplyDamageSkillAction : SkillAction
    {
       [HideInInspector] public UnitVariable Executor;
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

    }
}