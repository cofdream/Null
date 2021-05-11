using UnityEngine;

namespace Skill
{
    [System.Serializable]
    public class UnitBaseAttribute
    {
        public Numeric maxHealth;
        public NumericMax health;

        public Numeric atk;
        public Numeric def;
        public Numeric moveSpeed;

        //public IntNumeric attackDamage;
        //public IntNumeric abilityPower;

        public UnitBaseAttribute()
        {
            health = new NumericMax() { Dirty = true };
            maxHealth = new Numeric() { Dirty = true };
            atk = new Numeric() { Dirty = true };
            def = new Numeric() { Dirty = true };
            moveSpeed = new Numeric() { Dirty = true };

            health.Max = maxHealth;
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
                health.AddBaseValue(value);
            }
            else
            {
                health.AddBaseValuePercentage(value);
            }
        }

        public void AtkChange(int value, bool isBaseBalue)
        {
            if (isBaseBalue)
            {
                atk.AddBaseValue(value);
            }
            else
            {
                atk.AddBaseValuePercentage(value);
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