using System;
using UnityEngine;


[Serializable]
public class Person : DASerializable
{
    public string Name;
    public int Age;
    public Person Friend;
}
