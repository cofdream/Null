
namespace Skill
{
    [System.Serializable]
    public class NumericCompose
    {
        private Numeric current;
        public int Current
        {
            get
            {
                Calculate_CurrentValue();
                return current.Value;
            }
        }


        private Numeric max;
        public int Max
        {
            get
            {
                Calculate_MaxValue();
                return max.Value;
            }
        }


        public void Init(int currentValue, int maxValue)
        {
            current.BaseValue = currentValue;
            max.BaseValue = maxValue;
        }


        public void AddBaseValue_Current(int value)
        {
            current.AdditionBaseValue += value;
            current.Dirty = true;
        }
        public void AddBaseValuePercentage_Current(int value)
        {
            current.AdditionBaseValuePercentage += value;
            current.Dirty = true;
        }

        public void AddBaseValue_Max(int value)
        {
            max.AdditionBaseValue += value;
            max.Dirty = true;
        }
        public void AddBaseValuePercentage_Max(int value)
        {
            max.AdditionBaseValuePercentage += value;
            max.Dirty = true;
        }

        // todo Remove

        private void Calculate_CurrentValue()
        {
            if (current.Dirty)
            {
                int allBaseValue = current.BaseValue + current.AdditionBaseValue;
                int percentageValue = (int)(allBaseValue * (current.AdditionBaseValuePercentage / 100f + 0.005f));

                int value = allBaseValue + percentageValue;
                int maxValue = Max;

                current.Value = value > maxValue ? maxValue : value;

                current.Dirty = false;
            }
        }
        private void Calculate_MaxValue()
        {
            if (max.Dirty)
            {
                int allBaseValue = max.BaseValue + max.AdditionBaseValue;
                int percentageValue = (int)(allBaseValue * (max.AdditionBaseValuePercentage / 100f + 0.005f));

                max.Value = allBaseValue + percentageValue;

                max.Dirty = false;
            }
        }

        private static int PercentageCalculation(int value, int percentage)
        {
            return value + (int)(value * (percentage / 100f + 0.005f));
        }
    }
}