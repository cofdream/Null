using DA.AssetLoad;
using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testttttt : MonoBehaviour
{
    public AssetLoader loader;
    public ControllerGraph fsmGraph;

    public FiniteStateMachine fsm;
    void Start()
    {
        loader = AssetLoader.GetAssetLoader();

        fsmGraph = loader.LoadAsset<ControllerGraph>("Assets/XNode Examples/ControllerGraph.asset");

        fsm = (fsmGraph.nodes[0] as FSMNode).GetValue(null) as FiniteStateMachine;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        loader.UnloadAll();
    }
}
