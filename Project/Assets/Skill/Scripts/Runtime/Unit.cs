using Sirenix.OdinInspector;
using Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    [DisallowMultipleComponent]
    public class Unit : MonoBehaviour
    {
        public int HP = 100;
        public int Atk = 5;

        public IntNumeric hp;
        public IntNumeric atk;
        public IntNumeric speed;

        public List<Func<int, int>> HpBuffFunc = new List<Func<int, int>>();
        public List<Func<int, int>> AtkBuffFunc = new List<Func<int, int>>();

        public List<SKillBase> SKills;

        public Coroutine SkillCoroutine;

        [Button]
       public void AddSkill(SKillBase sKillBase)
        {
            SKills.Add(sKillBase);
        }

        [Button]
        public void CastSkill(int index)
        {
            SkillCoroutine = StartCoroutine(SKills[index].Cast());
        }
        [Button]
        public void StopSkill()
        {
            StopCoroutine(SkillCoroutine);
        }


        private void Update()
        {
            Debug.Log(gameObject.name +
                " ATK: " + CalculateATK() +
                " Hp:  " + CalculateHP()
                );
        }

        public int CalculateATK()
        {
            int value = Atk;
            foreach (var item in AtkBuffFunc)
            {
                if (item != null)
                {
                    value += item(Atk);
                }
            }

            return value;
        }
        public int CalculateHP()
        {
            int value = HP;
            foreach (var item in HpBuffFunc)
            {
                if (item != null)
                {
                    value += item(HP);
                }
            }

            return value;
        }
    }
}