﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tested2 : MonoBehaviour
{
    public Image sp;
    void Start()
    {
        sp.sprite = Resources.Load<Sprite>("Icons/img_icon_qq");
    }

    // Update is called once per frame
    void Update()
    {

    }
}