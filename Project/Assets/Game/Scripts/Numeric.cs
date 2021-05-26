
namespace Game
{
    [System.Serializable]
    public struct Numeric
    {
        [UnityEngine.SerializeField]
        private int value;
        [UnityEngine.SerializeField]
        private bool dirty;

        /// <summary>
        /// 基础值
        /// </summary>
        [UnityEngine.SerializeField]
        private int baseValue;
        /// <summary>
        /// 增加的基础值
        /// </summary>
        private int additionBaseValue;
        /// <summary>
        /// 增加的基础百分比值
        /// </summary>
        private int additionBaseValuePercentage;

        public Numeric(int baseValue)
        {
            this.baseValue = baseValue;

            value = baseValue;
            dirty = false;
            additionBaseValue = 0;
            additionBaseValuePercentage = 0;
        }

        public int GetValue()
        {
            if (dirty)
            {
                Calculate();
                dirty = false;
            }
            return value;
        }
        public int GetValue(int maxValue)
        {
            if (dirty)
            {
                Calculate(maxValue);
                dirty = false;
            }
            return value;
        }

        public void AddBaseValue(int value)
        {
            additionBaseValue += value;
            dirty = true;
        }
        public void AddBaseValuePercentage(int value)
        {
            additionBaseValuePercentage += value;
            dirty = true;
        }
        public void RemoveBaseValue(int value)
        {
            additionBaseValue -= value;
            dirty = true;
        }
        public void RemoveBaseValuePercentage(int value)
        {
            additionBaseValuePercentage -= value;
            dirty = true;
        }

        private void Calculate()
        {
            int allBaseValue = baseValue + additionBaseValue;
            int percentageValue = (int)(allBaseValue * (additionBaseValuePercentage / 100f + 0.005f));

            value = allBaseValue + percentageValue;
        }

        private void Calculate(int maxValue)
        {
            int allBaseValue = baseValue + additionBaseValue;
            int percentageValue = (int)(allBaseValue * (additionBaseValuePercentage / 100f + 0.005f));

            int curValue = allBaseValue + percentageValue;
            if (curValue > maxValue)
                value = maxValue;
            else
                value = curValue;
        }


        private static int PercentageCalculation(int value, int percentage)
        {
            return value + (int)(value * (percentage / 100f + 0.005f));
        }
    }

    // todo  监听值改变回调
    //public interface ITemp<T>
    //{
    //    T GetValue();
    //    T ValueChangeCallBak();
    //}
}