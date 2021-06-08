using UnityEngine;
using DA.Timer;
using System.Collections;

namespace Game.Skill
{
    [System.Serializable]
    public class SKill : ScriptableObject
    {
        public int Id;

        public string Name;
        public string Introduction;

        public SkillAction[] SkillActions;

        public GetTargetUnitType GetTargetUnitType;

        public virtual bool Cast(Unit castUnit)
        {
            return true;
        }

        public virtual void Init()
        {

        }

        public void CastSkill()
        {
            foreach (var skillAction in SkillActions)
            {
                skillAction.CastSkill();
            }
        }

        public void UpdateSkill(float delta)
        {
            foreach (var skillAction in SkillActions)
            {
                skillAction.UpdateAction(delta);
            }
        }
    }
}