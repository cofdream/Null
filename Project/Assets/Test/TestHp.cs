using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHp : MonoBehaviour
{
    public Camera uiCmaera;
    public Transform target;
    public RectTransform rectTransform;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = uiCmaera.WorldToScreenPoint(transform.position);
        pos.x = pos.x - Screen.width * 0.5f;
        pos.y = pos.y - Screen.height * 0.5f;

        rectTransform.anchoredPosition = pos;
    }
}
