using Game.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [DisallowMultipleComponent]
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private Image healthBg;
        [SerializeField] private Image healthFg;
        [SerializeField] private CanvasGroup canvasGroup;

        public Unit Unit;


        void LateUpdate()
        {
            healthFg.fillAmount = (float)Unit.UnitAttribute.Health / Unit.UnitAttribute.MaxHealth;

            UpdatePosition();
        }
        private void UpdatePosition()
        {
            if (Unit.GameObject.name == "Hero")
            {
                return;
            }

            Debug.Log(Unit.GameObject.name);
            Vector3 pos = Unit.GameObject.transform.position;

            Vector2 anchoredPosition = TestUIManager.Instance.HeroCamera.WorldToScreenPoint(pos);
            anchoredPosition.x = anchoredPosition.x - Screen.width * 0.5f;
            anchoredPosition.y = anchoredPosition.y - Screen.height * 0.5f;

            rectTransform.anchoredPosition = anchoredPosition;
        }
        internal void Init(Unit unit)
        {
            Unit = unit;

            UpdatePosition();
        }
    }
}
