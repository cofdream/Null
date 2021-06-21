using DA.Node;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NullNamespace
{
    public class OpenNodeData
    {
        [OnOpenAsset]
        public static bool OpenAssetNodeData(int instanceID, int line)
        {
            //var FiniteStateMachineData = EditorUtility.InstanceIDToObject(instanceID) as FiniteStateMachineData;
            //if (FiniteStateMachineData != null)
            //{
            //    NodeBaseEditor.OpenNode(FiniteStateMachineData);
            //    return true;
            //}

            var FiniteStateMachineDataGraph = EditorUtility.InstanceIDToObject(instanceID) as FiniteStateMachineDataGraph;
            if (FiniteStateMachineDataGraph != null)
            {
                NodeBaseEditor.OpenNode(FiniteStateMachineDataGraph);
                return true;
            }

            return false;
        }
    }
}