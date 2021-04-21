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

        public void Initialize()
        {
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