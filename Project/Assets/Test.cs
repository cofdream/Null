using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [ReorderableList]
    public int[] counrsss;
    [ReorderableList]
    public TestChild[] testChildren;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
[System.Serializable]
public class TestChild
{
    public int AAA;
    public string BBB;
}
[System.Serializable]

public class Test3
{
    public int BBB;
}