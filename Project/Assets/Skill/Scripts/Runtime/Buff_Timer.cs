using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NullNamespace
{
    public class Buff_Timer : MonoBehaviour
    {
        public bool RunState;
        public Unit targetUnit;
        public float WaitTime;
        public int ExecuteNumber;
        public  UnityAction Update;

        public string buffName_temp;

        private float currentTime;
        private int currentExecuteNumber;
        private bool needRemoveUpdate = false;


        private void Awake()
        {
            BuffsManager.Instance.OnUpdate.Add(OnUpdate);
            needRemoveUpdate = true;

            currentTime = 0;
            currentExecuteNumber = 0;
        }
        private void OnDestroy()
        {
            RemoveUpdate();
        }

        private void RemoveUpdate()
        {
            if (needRemoveUpdate == false) return;
            BuffsManager.Instance.OnUpdate.Remove(OnUpdate);
            needRemoveUpdate = false;

            Destroy(this);
        }

        public void OnUpdate(float dleta)
        {
            if (RunState == false) return;

            currentTime += dleta;
            if (currentTime >= WaitTime)
            {
                currentTime -= WaitTime;

                Update?.Invoke();

                currentExecuteNumber++;
                if (currentExecuteNumber >= ExecuteNumber)
                {
                    RemoveUpdate();
                }
            }
        }
    }
}