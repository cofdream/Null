using Game.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StateNode : Node
{
    public string StateName;
    public StateAction[] stateActions;

    [Output] public State State;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        State.StateName = StateName;
        State.StateAction = stateActions;

        return State;
    }
}