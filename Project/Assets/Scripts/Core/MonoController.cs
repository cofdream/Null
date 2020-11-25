using System;
using UnityEngine;

namespace DA
{
    public sealed class MonoController : MonoBehaviour
    {
        public static float DelateTime { get; private set; }

        public static event Action UpdataAction;
        void Update()
        {
            DelateTime = Time.deltaTime;

            UpdataAction?.Invoke();
        }
    }
}
