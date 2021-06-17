using System;

namespace Game
{
    [Serializable]
    public class Skill
    {
        public int Id;

        public string Name;
        public string Introduction;

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
    }
}