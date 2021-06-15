using System;
using UnityEngine;
using Game.Variable;

namespace Game
{
    [Serializable]
    public class Skill
    {
        public int Id;

        public string Name;
        public string Introduction;

        [HideInInspector] 
        public UnitVariable Executor;

        public SkillAction[] SkillActions;

        public void Init(Unit unit)
        {
            Executor.Value = unit;
        }
        public void CastSkill()
        {
            foreach (var skillAction in SkillActions)
            {
                if (skillAction.Active)
                {
                    skillAction.CastSkill();
                }
            }
        }

        public void UpdateSkill(float delta)
        {
            foreach (var skillAction in SkillActions)
            {
                if (skillAction.Active)
                {
                    skillAction.UpdateAction(delta);
                }
            }
        }
    }
}