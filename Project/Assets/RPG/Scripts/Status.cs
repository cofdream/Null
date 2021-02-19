using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public enum StatusType : ushort
    {
        Idle,
        Walk,
    }

    public class Status
    {
        public string Name;

        protected StatusType statusType;
        public virtual StatusType StatusType
        {
            get { return statusType; }
            set { statusType = value; }
        }

        public event UnityAction EnterAction;
        public event UnityAction UpdateAction;
        public event UnityAction ExitAction;
    }

    public class StatusController
    {
        public Status CurStatus;
        public Status LastStatus;

        public void Change(Status status)
        {

        }
    }

}