using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SkillAction
    {
        public bool Active = true;

        public string Name = "SkillAction";
        public string Introduction = "Null...";

        private bool isCastEnd;


        public virtual void CastSkill()
        {
            isCastEnd = false;
        }

        public virtual void UpdateAction(float delta)
        {

        }

        public virtual void CastEnd()
        {
            isCastEnd = true;
        }
    }
}