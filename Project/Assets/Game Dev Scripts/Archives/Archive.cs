using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Archive
{
    public string Name = null;

    public Vector3 Position;
    public Vector3 Rotation;

    public void Init()
    {

    }
}

public class Bag
{
    public Pet[] Pets;
}

public class Pet
{
    public int Id;

    public int Lv;

    public int Hp;
    public int MaxHp;

    public int WG;
    public int TG;
    public int WF;
    public int TF;
    public int S;

    /// <summary>
    /// 病毒
    /// </summary>
    public bool VirusStatus;

    public bool EggStatus;

    public int BallId;

    public int PetSex;

    public int[] EggGroup;

    // 缎带
}

public enum PetSex
{
    None = 0,
    Male,
    Female,
}

public enum EggGroup
{
    None = 0,
    Water,
    Land,
}