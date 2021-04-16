using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEmp : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    void Update()
    {
        gameObject.transform.position += direction * Time.deltaTime * speed;
    }
}
