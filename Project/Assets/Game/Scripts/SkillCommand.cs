using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SkillCommand
    {
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

        public void Execute(Unit castUnit)
        {
            if (Targets == null) return;

            foreach (var target in Targets)
            {
                foreach (var item in unitAttributeChanges)
                {
                    item.Change(target.UnitAttribute);
                }
            }
        }
    }
}