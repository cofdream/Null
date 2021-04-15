using UnityEngine;

namespace NullNamespace
{
    public interface ICastSpell
    {
        void CastSpell(Unit unit);
    }
    public class NormalCastSpell : ICastSpell
    {

        public void CastSpell(Unit unit)
        {
            var targetUnit = UnitsManager.Instance.GetRandomUnit(unit);

            targetUnit.HP -= unit.Atk;
        }
    }
}