using System;
using UnityEngine;
using DA.Timer;
using System.Collections;
using Game.Variable;

namespace Game.Skill
{
    [System.Serializable]
    public class SKill : ScriptableObject
    {
        public int Id;

        public string Name;
        public string Introduction;

        public UnitVariable Executor;

        public SkillAction[] SkillActions;

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

        public void Instantiate()
        {
           Executor = Instantiate(Executor);


        }
    }
}