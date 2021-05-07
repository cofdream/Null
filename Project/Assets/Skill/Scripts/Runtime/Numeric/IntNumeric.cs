
public class IntNumeric
{
    protected int value;
    /// <summary>
    /// 最终计算的值
    /// </summary>
    public int Value
    {
        get
        {
            if (dirty)
            {
                Calculate();
            }
            return value;
        }
        protected set => this.value = value;
    }
    /// <summary>
    /// 基础值
    /// </summary>
    public int BaseValue { get; protected set; }
    /// <summary>
    /// 增加的基础值
    /// </summary>
    public int AdditionBaseValue { get; protected set; }
    /// <summary>
    /// 增加的基础百分比值
    /// </summary>
    public int AdditionBaseValuePercentage { get; protected set; }


    /// <summary>
    /// 值改变标记
    /// </summary>
    protected bool dirty;


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
    protected virtual void Calculate()
    {
        var allBaseValue = BaseValue + AdditionBaseValue;
        value = allBaseValue + allBaseValue * AdditionBaseValuePercentage / 100;

        dirty = false;
    }
}
