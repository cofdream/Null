using UnityEngine;
using DA.Timer;
using System;
using System.Collections;

namespace Skill
{
    [System.Serializable]
    public class SKillBase
    {
        public float BeforeWaitTime;
        public float AfterWaitTime;

        public virtual IEnumerator Cast()
        {
            yield return new WaitForSeconds(BeforeWaitTime);

            DoSomething();

            yield return new WaitForSeconds(AfterWaitTime);
        }
        public virtual void DoSomething()
        {
            Debug.Log("Cast");
        }
    }
}