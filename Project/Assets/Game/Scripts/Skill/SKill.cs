using System;
using UnityEngine;
using DA.Timer;
using System.Collections;
using Game.Variable;
using System.Collections.Generic;

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

        public void InstantiateDependencies(Dictionary<ScriptableObject, ScriptableObject> AllDependencies)
        {
            if (AllDependencies.TryGetValue(Executor, out ScriptableObject scriptableObject))
            {
                Executor = scriptableObject as UnitVariable;

                foreach (var skillAction in SkillActions)
                {
                    skillAction.InstantiateDependencies(AllDependencies);
                }
            }
            else
            {
                Debug.LogError("-------------");
            }
        }
    }
}