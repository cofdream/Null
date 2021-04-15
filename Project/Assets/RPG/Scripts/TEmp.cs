using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEmp : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.forward * Time.deltaTime * speed;
    }
}
