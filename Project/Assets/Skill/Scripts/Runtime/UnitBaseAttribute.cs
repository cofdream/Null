namespace Skill
{
    [System.Serializable]
    public class UnitBaseAttribute
    {
        [UnityEngine.SerializeField]
        private Numeric maxHealth;
        [UnityEngine.SerializeField]
        private Numeric health;

        [UnityEngine.SerializeField]
        private Numeric atk;
        [UnityEngine.SerializeField]
        private Numeric def;
        [UnityEngine.SerializeField]
        private Numeric moveSpeed;


        public int Health => health.GetValue(maxHealth.GetValue());
        public int MaxHealth => maxHealth.GetValue();
        public int Atk => atk.GetValue();
        public int Def => def.GetValue();
        public int MoveSpeed => moveSpeed.GetValue();


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
}