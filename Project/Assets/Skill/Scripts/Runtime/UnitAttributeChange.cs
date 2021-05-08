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

        public bool isBaseBalue = true;
        public int valueInt;
        public float valueFlot;
        public void Change(UnitBaseAttribute attribute)
        {
            switch (changType)
            {
                case AttributeChangeType.Healtgh:
                    if (isBaseBalue)
                        attribute.health.AddBaseValue(valueInt);
                    else
                        attribute.health.AddBaseValuePercentage(valueInt);
                    break;
                case AttributeChangeType.Atk:
                    if (isBaseBalue)
                        attribute.atk.AddBaseValue(valueInt);
                    else
                        attribute.atk.AddBaseValuePercentage(valueInt);
                    break;
                case AttributeChangeType.Def:
                    if (isBaseBalue)
                        attribute.def.AddBaseValue(valueInt);
                    else
                        attribute.def.AddBaseValuePercentage(valueInt);
                    break;
                case AttributeChangeType.MoveSpeed:
                    if (isBaseBalue)
                        attribute.moveSpeed.AddBaseValue(valueInt);
                    else
                        attribute.moveSpeed.AddBaseValuePercentage(valueInt);
                    break;
            }
        }
    }
}