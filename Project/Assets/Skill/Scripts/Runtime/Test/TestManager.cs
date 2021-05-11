using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class TestManager : MonoBehaviour
    {
        public List<Unit> AllUnit;

        public TestUIManager TestUIManager;

        [Button]
        public void CreateUnit(Unit unit)
        {
            AllUnit.Add(unit);
        }

        private void Start()
        {
            TestUIManager.InitUnits(AllUnit);
        }
    }
}