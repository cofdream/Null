using DA.Singleton;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skill
{
    public class TestUIManager : MonoBehaviour
    {
        public UIUnit uiUnit;
        public ScrollRect unitsScrollView;

        public UISkill uiSkill;
        public ScrollRect skillsScrollView;

        public DrawUnitAttribute drawUnitAttribute;

        public TestManager TestManager;

        public static Unit selectUnit;
        public static TestUIManager Instance;

        private void Awake()
        {
            Instance = this;
        }


        public void ReLoadUnits(List<Unit> allUnit)
        {
            foreach (Transform item in unitsScrollView.content.transform)
            {
                Destroy(item.gameObject);
            }

            foreach (var unit in allUnit)
            {
                var temp = Instantiate(uiUnit, unitsScrollView.content.transform);
                temp.Init(unit);
            }

            SelectUnit(allUnit[0]);
        }

        public static void SelectUnit(Unit unit)
        {
            if (unit != null && unit != selectUnit)
            {
                selectUnit = unit;

                Instance.InitSkill(selectUnit);

                Instance.drawUnitAttribute.Unit = selectUnit;
            }
        }

        private void InitSkill(Unit selectUnit)
        {
            foreach (Transform item in skillsScrollView.content.transform)
            {
                Destroy(item.gameObject);
            }
            foreach (var sKill in selectUnit.SKills)
            {
                var temp = Instantiate(uiSkill, skillsScrollView.content.transform);
                temp.Init(sKill);
                sKill.Init();
            }
        }
    }
}