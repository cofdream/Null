using Sirenix.OdinInspector;
using UnityEngine;

namespace Skill
{
    [System.Serializable]
    public class SkillCommand
    {
        //public Unit CastUnit;

        public Unit[] Targets;

        public UnitAttributeChange[] unitAttributeChanges;

        public void GetTarget(Unit target)
        {
            Targets = new Unit[] { target };
        }
        public void GetTarget(Unit[] targets)
        {
            Targets = targets;
        }

        public void Execute()
        {
            foreach (var target in Targets)
            {
                foreach (var item in unitAttributeChanges)
                {
                    item.Change(target.unitBaseAttribute);
                }
            }
        }
    }
}