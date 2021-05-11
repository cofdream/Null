
namespace Skill
{
    [System.Serializable]
    public class Numeric
    {
        /// <summary>
        /// 最终计算的值
        /// </summary>
        public int Value;
        /// <summary>
        /// 值改变标记
        /// </summary>
        public bool Dirty;

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
    }
}