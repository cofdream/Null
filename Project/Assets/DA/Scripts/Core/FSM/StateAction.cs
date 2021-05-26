

namespace DA.Core.FSM
{
    [System.Serializable]
    public class StateAction 
    {
        public bool Active;

        public virtual void OnEnter()
        {

        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnFixedUpdate()
        {

        }
        public virtual void OnExit()
        {

        }
    }
}