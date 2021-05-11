
namespace Skill
{
    [System.Serializable]
    public class Numeric
    {

        /// <summary>
        /// 最终计算的值
        /// </summary>
        [UnityEngine.HideInInspector] public int Value;
        /// <summary>
        /// 值改变标记
        /// </summary>
        [UnityEngine.HideInInspector] public bool Dirty;

        /// <summary>
        /// 基础值
        /// </summary>
        public int BaseValue;
        /// <summary>
        /// 增加的基础值
        /// </summary>
        [UnityEngine.HideInInspector] public int AdditionBaseValue;
        /// <summary>
        /// 增加的基础百分比值
        /// </summary>
        [UnityEngine.HideInInspector] public int AdditionBaseValuePercentage;


        public int GetValue()
        {
            Calculate();
            return Value;
        }
        public int GetValue(int max)
        {
            Calculate();
            if (Value > max)
            {
                Value = max;
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
        // todo Remove

        private void Calculate()
        {
            if (Dirty)
            {
                int allBaseValue = BaseValue + AdditionBaseValue;
                int percentageValue = (int)(allBaseValue * (AdditionBaseValuePercentage / 100f + 0.005f));

                Value = allBaseValue + percentageValue;

                Dirty = false;
            }
        }


        private static int PercentageCalculation(int value, int percentage)
        {
            return value + (int)(value * (percentage / 100f + 0.005f));
        }
    }
}