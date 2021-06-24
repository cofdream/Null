using Game.FSM;
using System;
using System.Reflection;
using UnityEngine;

namespace NullNamespace
{
    [Serializable]
    public class FiniteStateMachineDataGraph : ScriptableObject
    {
        public FiniteStateMachineData FiniteStateMachineData;

        public Rect NodeRect = new Rect(0, 0, 250, 150);

        public bool Toogle;
        public StateDataGraph[] stateDataGraphs;

        public void LoadData()
        {
            int length = FiniteStateMachineData.StateDatas.Length;
            if (stateDataGraphs.Length < length)
            {
                var temp = new StateDataGraph[length];
                stateDataGraphs.CopyTo(temp, 0);
            }
            for (int i = 0; i < length; i++)
            {
                if (stateDataGraphs[i] == null)
                {
                    stateDataGraphs[i] = new StateDataGraph();
                    stateDataGraphs[i].StateData = FiniteStateMachineData.StateDatas[i];
                }
            }
        }

        public void AddState()
        {
            var statData = new StateData();
            var stateDataGraph = new StateDataGraph() { StateData = statData };

            int length = FiniteStateMachineData.StateDatas.Length + 1;

            var temp = new StateDataGraph[length];
            stateDataGraphs.CopyTo(temp, 0);
            stateDataGraphs = temp;
            stateDataGraphs[length - 1] = stateDataGraph;

            var temp2 = new StateData[length];
            FiniteStateMachineData.StateDatas.CopyTo(temp2, 0);
            FiniteStateMachineData.StateDatas = temp2;
            FiniteStateMachineData.StateDatas[length - 1] = statData;
        }
    }
}