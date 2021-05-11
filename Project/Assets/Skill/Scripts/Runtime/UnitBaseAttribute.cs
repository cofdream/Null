using UnityEngine;

namespace Skill
{
    [System.Serializable]
    public class UnitBaseAttribute
    {
        public Numeric health;
        public Numeric health_Max;

        public Numeric atk;
        public Numeric def;
        public Numeric moveSpeed;

        //public IntNumeric attackDamage;
        //public IntNumeric abilityPower;


        public void HealthRestore()
        {

        }
        public void HealthLoss()
        {

        }

        public void HealthChange(int value, bool isBaseBalue)
        {
            if (isBaseBalue)
            {
                health.BaseValue += value;
            }
            else
            {
                health.BaseValue = PercentageCalculation(health.BaseValue, value);
            }
            //todo 溢出
        }

        public static int PercentageCalculation(int value, int percentage)
        {
            return value + (int)(value * ( percentage / 100f + 0.005f));
        }
    }
}