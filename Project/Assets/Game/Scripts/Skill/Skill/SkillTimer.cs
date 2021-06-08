using DA.Timer;
using System;
using UnityEngine;

namespace Game.Skill
{
    public class SkillTimer : SKill
    {
        public float CastStartWait;
        public float CastEndWait;

        public float SkillCD;

        private bool isInCD;

        private Unit castUnit;

        public override void Init()
        {
            isInCD = false;
        }
        public override bool Cast(Unit castUnit)
        {
            if (!isInCD)
            {
                isInCD = true;
                this.castUnit = castUnit;

                Timer.GetTimer().Run(SkillCD, UpdateCD);

                TimerCoroutine.GetTimer().Run(CastStartWait, StartSkill);

                return true;
            }

            return false;
        }

        private void UpdateCD()
        {
            isInCD = false;
        }
        private bool StartSkill(TimerCoroutine timer)
        {
            Execute();

            timer.WaitingTime = CastEndWait;
            timer.Callback = EndSkill;
            return false;
        }
        private bool EndSkill(TimerCoroutine timer)
        {
            return true;
        }

        protected void Execute()
        {
            Unit[] targetUnits = null;
            if (GetTargetUnitType == GetTargetUnitType.Self)
            {
                targetUnits = new Unit[] { castUnit };
            }

            foreach (var command in SkillActions)
            {
                command.Targets = targetUnits;
                command.Execute(castUnit);
                command.Targets = null;
            }
        }
    }
}