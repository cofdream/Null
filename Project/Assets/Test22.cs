using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DA;

public class Test22 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TT2.Instance.Init();
        TT2.Instance.Init();
        TT2.Instance.Init();

        TT3.Instance.Init();
        TT3.Instance.Init();
        TT3.Instance.Init();


    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class TT2 : Singleton<TT2>
{
    int i = 0;
    TT2() { }
    public void Init()
    {
        Debug.Log(i++);
    }
}

public class TT3 : Singleton<TT3>
{
    int i = 0;
    TT3() { }
    public void Init()
    {
        Debug.Log(i++);
    }
}


public class TT4
{
    static TT4 instance;
    public static TT4 Instance
    {

        get { return instance; }
    }

    static TT4()
    {
        instance = Singleton.GetSingleton<TT4>();
    }
}