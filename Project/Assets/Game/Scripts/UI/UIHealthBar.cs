using Game.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [DisallowMultipleComponent]
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private Image healthBg;
        [SerializeField] private Image healthFg;
        [SerializeField] private CanvasGroup canvasGroup;

        public Unit Unit;


        void LateUpdate()
        {
            healthFg.fillAmount = (float)Unit.UnitAttribute.Health / Unit.UnitAttribute.MaxHealth;

            CalculateHealthBarPosition();
        }
        private void CalculateHealthBarPosition()
        {
            //Camera camera = TestUIManager.Instance.HeroCamera;

            //Vector3 pos = Unit.ControllerHangPoint.HeadTop.position;

            //Vector2 position = camera.WorldToScreenPoint(pos);
            //position.x = position.x - Screen.width * 0.5f;
            //position.y = position.y - Screen.height * 0.5f;

            //Vector3 position = camera.WorldToViewportPoint(pos);
            //if (position.z < 0)
            //{
            //    return;
            //}
            //position.x = (position.x - 0.5f) * Screen.width;
            //position.y = (position.y - 0.5f) * Screen.height;

            //rectTransform.anchoredPosition = position;
        }
        internal void Init(Unit unit)
        {
            Unit = unit;

            CalculateHealthBarPosition();
        }
    }
}
