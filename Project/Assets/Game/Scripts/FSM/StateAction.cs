

namespace Game.FSM
{
    [System.Serializable]
    public class StateAction : ScriptableObjectClone
    {
        public bool Active = true;

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