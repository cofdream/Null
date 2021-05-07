using DA.Timer;

namespace Skill
{
    public class SkillTimer : SKillBase
    {
        public float SkillCD;
        private bool isInCD;
        
        public bool Cast()
        {
            if (!isInCD)
            {
                isInCD = true;
                Timer.GetTimer().Run(SkillCD, UpdateCD);

                Cast3();

                return true;
            }
            return false;
        }

        private void UpdateCD()
        {
            isInCD = false;
        }

        public void Cast3()
        {

        }
    }
}