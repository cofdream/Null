using DA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTT : MonoBehaviour
{
    IObjectPoolTime<Data> objectPool;

    int nn = 1;

    void Start()
    {
        var pool = new ObjectPoolTimer<Data>();
        pool.Count = 10;
        objectPool = pool;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            objectPool.Get();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            objectPool.Set(new Data() { name = nn++.ToString() });
            objectPool.Set(new Data() { name = nn++.ToString() });
            objectPool.Set(new Data() { name = nn++.ToString() });
        }

        objectPool.Update(Time.deltaTime);
    }
}

public class Data : IPoolObjectTime
{
    public string name;

    public float LeftCacheTime { get; set; }
    public float CacheTime { get; set; } = 1;

    public void Free()
    {
        Debug.Log($"Free {name}");
    }
}

