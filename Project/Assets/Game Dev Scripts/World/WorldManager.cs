using DA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WorldManager
{
    private static WorldManager instance = DA.Singleton.SingletonUtil.QuickCreateSingleton<WorldManager>();
    public static WorldManager Instance { get { return instance; } }
    [System.Obsolete("Not Use", true)]
    public WorldManager() { }

    public void EnterWorld()
    {

    }
}
