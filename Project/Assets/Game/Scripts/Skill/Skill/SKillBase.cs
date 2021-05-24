using UnityEngine;
using DA.Timer;
using System.Collections;

namespace Game.Skill
{
    [System.Serializable]
    public class SKillBase 
    {
        public int Id;

        public string Name;
        public string Introduction;

        public SkillCommand[] Commands;

        public GetTargetUnitType GetTargetUnitType;

        public virtual bool Cast(Unit castUnit)
        {
            return true;
        }

        public virtual void Init()
        {

        }

        public void UpdateSkill(float delte)
        {

        }
    }
}