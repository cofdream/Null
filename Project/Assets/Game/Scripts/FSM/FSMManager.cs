using Game.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FSMManager : MonoBehaviour
    {
        private static List<FSM.FiniteStateMachine> fsmList = new List<FSM.FiniteStateMachine>();
        private static FSMManager instance;
        public static GlobalVariabeles GlobalVariabeles { get; set; }

        // todo set Awake order
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }

            GlobalVariabeles = new GlobalVariabeles();
        }


        void Update()
        {
            GlobalVariabeles.DeltaTimeVariables.Value = Time.deltaTime;

            foreach (var fsm in fsmList)
            {
                fsm.OnUpdate();
            }
        }
        void FixedUpdate()
        {
            foreach (var fsm in fsmList)
            {
                fsm.OnFixedUpdate();
            }
        }

        public static void AddFSM(FSM.FiniteStateMachine fsm)
        {
            fsmList.Add(fsm);
        }
        public static void RemoveFSM(FSM.FiniteStateMachine fsm)
        {
            fsmList.Remove(fsm);
        }
    }
}