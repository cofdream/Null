using UnityEngine;

namespace Test
{
    [System.Serializable]
    public enum RoleAttributeType
    {
        ATK,
        HP,
    }
    public class BuffBase : MonoBehaviour
    {
        public RoleAttributeType roleAttributeType;
        public Unit unit;

        public bool isPercentage;
        public int addValue = 1;

        private void Awake()
        {
            unit = GetComponent<Unit>();

            switch (roleAttributeType)
            {
                case RoleAttributeType.ATK:
                    unit.AtkBuffFunc.Add(CalculateValue);
                    break;
                case RoleAttributeType.HP:
                    unit.HpBuffFunc.Add(CalculateValue);
                    break;
            }
        }

        private int CalculateValue(int value)
        {
            if (isPercentage)
            {
                return (int)((value * addValue * 0.01) + 0.9999);
            }
            else
            {
                return addValue;
            }
        }
    }
}