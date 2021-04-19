using System;
using System.Collections.Generic;
using UnityEngine;

namespace NullNamespace
{
    public delegate void UpdateBuff(float delta);
    public class BuffsManager : MonoBehaviour
    {
        public static BuffsManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Instance != null");
            }
            Instance = this;
        }

        public List<UpdateBuff> OnStartUpdate = new List<UpdateBuff>() { };
        public List<UpdateBuff> OnUpdate = new List<UpdateBuff>() { };
        public List<UpdateBuff> OnEndUpdate = new List<UpdateBuff>() { };

        private void Update()
        {
            float delta = Time.deltaTime;

            CallAllElement(OnStartUpdate, delta);

            CallAllElement(OnUpdate, delta);

            CallAllElement(OnEndUpdate, delta);
        }

        private void CallAllElement(List<UpdateBuff> list, float delta)
        {
            for (int i = list.Count - 1; i > -1; i--)
            {
                list[i]?.Invoke(delta);
            }
        }
    }
}
