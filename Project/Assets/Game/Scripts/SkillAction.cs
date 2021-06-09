using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SkillAction : ScriptableObject
    {
        public bool Active = true;

        public string Name = "SkillAction";
        public string Introduction = "Null...";


        public virtual void CastSkill()
        {

        }

        public virtual void UpdateAction(float delta)
        {

        }
    }
}