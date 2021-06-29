using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Magic
    {
        public UnitAttribute UnitAttribute;

        public Skill[] Skills = new Skill[0];

        public List<Skill> CastSkill;

        public void Cast(int index)
        {
            if (index > 0 && index < Skills.Length)
            {
                Skills[index].CastSkill();
            }
        }

        public void Update()
        {
            foreach (var skill in Skills)
            {
                skill.UpdateSkill(Time.deltaTime);
            }
        }
    }
}