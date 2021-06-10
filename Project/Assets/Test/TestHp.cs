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
        Vector3 position = uiCmaera.WorldToScreenPoint(transform.position);
        position.x = (position.x - Screen.width * 0.5f);
        position.y = (position.y - Screen.height * 0.5f);

        rectTransform.anchoredPosition = position;
    }
}
