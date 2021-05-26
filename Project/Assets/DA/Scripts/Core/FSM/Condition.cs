﻿

namespace DA.Core.FSM
{
    [System.Serializable]
    public class Condition 
    {
        public string Description;
        public virtual bool CheckStateChange()
        {
            return false;
        }
    }
}