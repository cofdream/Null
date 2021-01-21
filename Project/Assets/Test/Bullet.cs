using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool fire = false;
    private Vector3 direction;
    private float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void Fire(Vector3 position, Vector3 direction, float speed, Action<Bullet> action)
    {
        transform.position = position;
        this.direction = direction;
        this.speed = speed;
        fire = true;

        DA.Timer.Timer.StartTimer(new DA.Timer.TimerDisposable(3, () =>
        {
            fire = false;
            action(this);
        }));
    }
}
