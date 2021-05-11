using UnityEngine;

namespace Skill
{
    public enum AttributeChangeType
    {
        Healtgh,
        Atk,
        Def,
        MoveSpeed,
    }

    [System.Serializable]
    public class UnitAttributeChange
    {
        public AttributeChangeType changType;

        public bool isBaseValue = true;
        public int valueInt;

        public void Change(UnitBaseAttribute attribute)
        {
            switch (changType)
            {
                case AttributeChangeType.Healtgh:
                    attribute.HealthChange(valueInt, isBaseValue);
                    break;
                //case AttributeChangeType.Atk:
                //    if (isBaseValue)
                //        attribute.atk.AddBaseValue(valueInt);
                //    else
                //        attribute.atk.AddBaseValuePercentage(valueInt);
                //    break;
                //case AttributeChangeType.Def:
                //    if (isBaseValue)
                //        attribute.def.AddBaseValue(valueInt);
                //    else
                //        attribute.def.AddBaseValuePercentage(valueInt);
                //    break;
                //case AttributeChangeType.MoveSpeed:
                //    if (isBaseValue)
                //        attribute.moveSpeed.AddBaseValue(valueInt);
                //    else
                //        attribute.moveSpeed.AddBaseValuePercentage(valueInt);
                //    break;
            }
        }
    }
}