
namespace Skill
{
    [System.Serializable]
    public struct Numeric
    {
        [UnityEngine.SerializeField]
        private int Value;
        [UnityEngine.SerializeField]
        private bool Dirty;

        /// <summary>
        /// 基础值
        /// </summary>
        [UnityEngine.SerializeField]
        private int BaseValue;
        /// <summary>
        /// 增加的基础值
        /// </summary>
        private int AdditionBaseValue;
        /// <summary>
        /// 增加的基础百分比值
        /// </summary>
        private int AdditionBaseValuePercentage;

        public int GetValue()
        {
            if (Dirty)
            {
                Calculate();
                Dirty = false;
            }
            return Value;
        }
        public int GetValue(int maxValue)
        {
            if (Dirty)
            {
                Calculate(maxValue);
                Dirty = false;
            }
            return Value;
        }

        public void AddBaseValue(int value)
        {
            AdditionBaseValue += value;
            Dirty = true;
        }
        public void AddBaseValuePercentage(int value)
        {
            AdditionBaseValuePercentage += value;
            Dirty = true;
        }
        public void RemoveBaseValue(int value)
        {
            AdditionBaseValue -= value;
            Dirty = true;
        }
        public void RemoveBaseValuePercentage(int value)
        {
            AdditionBaseValuePercentage -= value;
            Dirty = true;
        }

        private void Calculate()
        {
            int allBaseValue = BaseValue + AdditionBaseValue;
            int percentageValue = (int)(allBaseValue * (AdditionBaseValuePercentage / 100f + 0.005f));

            Value = allBaseValue + percentageValue;
        }

        private void Calculate(int maxValue)
        {
            int allBaseValue = BaseValue + AdditionBaseValue;
            int percentageValue = (int)(allBaseValue * (AdditionBaseValuePercentage / 100f + 0.005f));

            int curValue = allBaseValue + percentageValue;
            if (curValue > maxValue)
                Value = maxValue;
            else
                Value = curValue;
        }


        private static int PercentageCalculation(int value, int percentage)
        {
            return value + (int)(value * (percentage / 100f + 0.005f));
        }
    }
}