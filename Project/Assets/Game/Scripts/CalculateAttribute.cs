using UnityEngine;

namespace Game.Test
{
    public enum IncreaseType
    {
        Fixed,//固定值
        Percentage,//百分比
    }
    public class CalculateAttribute
    {
        public IncreaseType IncreaseType;

        public int IncreaseValue;

        public int Value;
    }
}