using System;
using UnityEngine;

namespace DA
{
    public sealed class MonoController : MonoBehaviour
    {
        private static float delateTime;
        public static float DelateTime { get => delateTime; private set => delateTime = value; }

        public static event Action UpdataAction;

        void Update()
        {
            DelateTime = Time.deltaTime;

            UpdataAction?.Invoke();
        }
    }
}
