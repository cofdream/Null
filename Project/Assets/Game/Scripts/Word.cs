using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Word : MonoBehaviour
    {
        public MonoBehaviour unitPrefab;

        Unit unit;
        void Start()
        {
            unit = new Unit();
            unit.Initialize(Instantiate<MonoBehaviour>(unitPrefab, transform));
        }

        void Update()
        {
            
        }
    }
}