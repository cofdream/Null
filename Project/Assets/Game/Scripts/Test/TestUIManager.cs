using DA.AssetLoad;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Test
{
    public class TestUIManager : MonoBehaviour
    {
        public static TestUIManager Instance { get; private set; }


        public UIUnit uiUnit;
        public ScrollRect unitsScrollView;

        public UISkill uiSkill;
        public ScrollRect skillsScrollView;

        public DrawUnitAttribute drawUnitAttribute;

        public Transform helthTransform;
        public string HealthPerabPath;

        public static Unit selectUnit;

        public Camera HeroCamera;

        private void Awake()
        {
            Instance = this;
        }


        public void ReLoadUnits(List<Unit> allUnit)
        {
            var loader = AssetLoader.GetAssetLoader();

            var healthPrefab = loader.LoadAsset<UIHealthBar>(HealthPerabPath);
            foreach (var unit in allUnit)
            {
                var temp = Instantiate(uiUnit, unitsScrollView.content.transform);
                temp.Init(unit);

                var uiHealth = Instantiate(healthPrefab, helthTransform.transform);
                uiHealth.Init(unit);
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
            foreach (var sKill in selectUnit.Skills)
            {
                var temp = Instantiate(uiSkill, skillsScrollView.content.transform);
                temp.Init(sKill);
            }
        }
    }
}