
namespace Skill
{
    [System.Serializable]
    public struct Numeric
    {
        [Sirenix.OdinInspector.ReadOnly, UnityEngine.SerializeField]
        private int value;
        /// <summary>
        /// 值改变标记
        /// </summary>
        private bool dirty;

        /// <summary>
        /// 基础值
        /// </summary>
        public int BaseValue;
        /// <summary>
        /// 增加的基础值
        /// </summary>
        public int AdditionBaseValue;
        /// <summary>
        /// 增加的基础百分比值
        /// </summary>
        public int AdditionBaseValuePercentage;


        /// <summary>
        /// 最终计算的值
        /// </summary>
        public int Value
        {
            get
            {
                Calculate();
                return value;
            }
        }


        public void Init(int value)
        {
            BaseValue = value;

            dirty = false;
        }
        public void AddBaseValue(int value)
        {
            AdditionBaseValue += value;
            dirty = true;
        }
        public void AddBaseValuePercentage(int value)
        {
            AdditionBaseValuePercentage += value;
            dirty = true;
        }
        // todo Remove

        public void Calculate()
        {
            if (dirty)
            {
                int allBaseValue = BaseValue + AdditionBaseValue;
                int percentageValue = (int)(allBaseValue * (AdditionBaseValuePercentage / 100f + 0.005f));

                value = allBaseValue + percentageValue;

                dirty = false;
            }
        }
    }
}