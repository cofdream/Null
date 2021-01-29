using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBind : MonoBehaviour
{
    public Text nameText;
    private string Name = "Node";
    private void Awake()
    {
        nameText.text = Name;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
