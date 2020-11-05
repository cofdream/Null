﻿using DA.UI;
using UnityEngine;

public class SetNameWindow : UIWindow
{
    SetNameWindowBind bind;
    public override void Awake()
    {
        bind.buttonEnd.onClick.AddListener(EndInputName);
    }

    public override void OnDestory()
    {
        bind.buttonEnd.onClick.RemoveListener(EndInputName);
    }

    public override void OnDisable()
    {

    }

    public override void OnEnable()
    {

    }

    public override void SetContext(Object context)
    {
        this.bind = context as SetNameWindowBind;
    }

    public void EndInputName()
    {
        ArchivesData.Instance.SaveName(bind.inputFieldName.text);

    }
}