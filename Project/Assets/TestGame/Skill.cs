using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Skill
{
    public int damage;

    public SKillNode[] nodes;

    public Skill()
    {
        nodes = new SKillNode[3];
        nodes[2] = new NodeWaitTime() { waitTime = 0.2f };
        nodes[1] = new NodeGetAllGameObject() { nextStep = nodes[2].Run };
        nodes[0] = new NodeWaitTime() { waitTime = 0.5f, nextStep = nodes[1].Run };
    }

    public void Cast()
    {
        if (nodes == null || nodes.Length < 1)
        {
            return;
        }
        nodes[0].Run();
    }
}
