using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    void Start()
    {
        


        //System.MulticastDelegate

        
        System.Action a = new System.Action(AA);
        a.Invoke();

        System.Delegate @delegate = a;
        a.Invoke();


        var t = Resources.Load<TextAsset>("A");


        var t1 = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/DAFrame/Test/Resources/A.bytes");
        var t2 = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/DAFrame/Test/Resources/A.txt");

        Debug.Log(1);
    }

    void AA()
    {
        Debug.Log(1);
    }

    void Update()
    {

    }
}