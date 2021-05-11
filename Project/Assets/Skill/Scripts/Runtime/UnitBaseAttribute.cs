using UnityEngine;

namespace Skill
{
    [System.Serializable]
    public class UnitBaseAttribute
    {
        public NumericCompose health;

        public Numeric atk;
        public Numeric def;
        public Numeric moveSpeed;

        //public IntNumeric attackDamage;
        //public IntNumeric abilityPower;

        public UnitBaseAttribute()
        {

        }


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
                health.AddBaseValue_Max(value);
            }
            else
            {
                health.AddBaseValuePercentage_Max(value);
            }
        }
    }
    public class NumericChange
    {
        public bool IsPercentage = false;
        public int ChangeValue;


        public string description;
    }
}