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

        }


        [Button]
        private void ReadloadUnits()
        {
            TestUIManager.ReLoadUnits(AllUnit);
        }
    }
}