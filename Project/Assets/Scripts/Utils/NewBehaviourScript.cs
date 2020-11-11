using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


public class Data2
{
    int count;
    int Count
    {
        set
        {
            if (value != count)
            {
                count = value;
                //bind.Btn_Login.name = value.ToString();
            }
        }
    }
    bool isOpen;


    //DD<string> dD  = new DD<string>() { action = };
    //DD<int> dD2;

    //public LoginWindowBind bind;

    //public void A()
    //{
    //    //dD.action = bind.Btn_Login.name;


    //    dD.Set((10).ToString());
    //    dD2.Set(1);
    //}

}

public class Windows
{
    public Text text;
}

public struct DD<T>
{
    public Action<T> action;
    public void Set(T t)
    {
        action(t);
    }
}
