using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class TestManager : MonoBehaviour
    {

        public SKillBase[] AllSkills;

        public List<Unit> AllUnit;

        public TestUIManager TestUIManager;

        [Button]
        public void CreateUnit(Unit unit)
        {
            AllUnit.Add(unit);
        }


        private void CreateUnit()
        {
            Unit unit = new Unit();
            unit.Name = Random.Range(0, 10).ToString();
            unit.sKills = new SKillBase[]
            {
                AllSkills[Random.Range(0,AllSkills.Length)],
            };
            unit.unitBaseAttribute = new UnitBaseAttribute();
            unit.unitBaseAttribute.health.BaseValue = Random.Range(1, 51);
            unit.unitBaseAttribute.maxHealth.BaseValue = 100;

            unit.unitBaseAttribute.atk.BaseValue = Random.Range(0, 50);
            unit.unitBaseAttribute.def.BaseValue = Random.Range(0, 50);
            unit.unitBaseAttribute.moveSpeed.BaseValue = Random.Range(0, 50);


            AllUnit.Add(unit);
        }


        private void Start()
        {
            AllUnit = new List<Unit>();
            CreateUnit();
            CreateUnit();
            CreateUnit();

            TestUIManager.InitUnits(AllUnit);
        }
    }
}