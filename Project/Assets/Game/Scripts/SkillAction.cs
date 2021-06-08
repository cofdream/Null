using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SkillAction : ScriptableObject
    {
        public bool Active;

        public string Name;
        public string Introduction;


        public void CastSkill()
        {

        }

        public void UpdateAction(float delta)
        {

        }
    }
}