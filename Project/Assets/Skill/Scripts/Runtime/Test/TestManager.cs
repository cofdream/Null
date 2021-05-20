using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class TestManager : MonoBehaviour
    {
        public List<Unit> AllUnit;

        public TestUIManager TestUIManager;

        private void Start()
        {
            foreach (var unit in AllUnit)
            {
                unit.Init();
            }
        }
    }
}