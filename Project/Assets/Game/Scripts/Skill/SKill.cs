using System;
using UnityEngine;
using Game.Variable;
using System.Collections.Generic;

namespace Game.Skill
{
    [System.Serializable]
    public class Skill : DAScriptableObject
    {
        public int Id;

        public string Name;
        public string Introduction;

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


        public override void CloneVariables(Dictionary<int, CloneData> allDependencies)
        {
            Executor = GetCloneInstance(allDependencies, Executor);

            for (int i = 0; i < SkillActions.Length; i++)
            {
                var skillAction = GetCloneInstance(allDependencies, SkillActions[i]);
                skillAction.CloneVariables(allDependencies);

                SkillActions[i] = skillAction;
            }
        }
    }
}