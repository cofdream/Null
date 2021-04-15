using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    [DisallowMultipleComponent]
    public class Unit : MonoBehaviour
    {
        public int HP;
        public int Atk;

        public List<Unit> targetUnits = new List<Unit>();

        public int Damage;

        public void AddTargetUnit(Unit target)
        {
            foreach (var item in targetUnits)
            {
                if (item == target)
                {
                    return;
                }
            }
            targetUnits.Add(target);
        }

        public NormalCastSpell castSpell;

        public void Initialize()
        {
            castSpell = new NormalCastSpell();

            HP = 100;
            Atk = 5; 
        }

        public void AddHp(int value)
        {
            HP += value;
        }
        public void AddAtK(int value)
        {
            Atk += value;
        }
    }
}