using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class FSMNode : Node
{
    [Input] public State[] states;
    [Input] public State State;

    FiniteStateMachine fsm;


    public override object GetValue(NodePort port)
    {
        fsm.AllStates = GetInputValues<State>("states"); ;
        fsm.CurrentState = GetInputValue<State>("State"); ;

        return fsm;
    }
}
