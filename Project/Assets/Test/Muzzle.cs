using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject bulletGameObject;
    public DA.ObjectPool.ObjectPool<Bullet> bulletPool;
    void Start()
    {
        bulletPool = new DA.ObjectPool.ObjectPool<Bullet>();
        bulletPool.Initialize(OnCreateBullet, OnDestoryBullet);
    }

    float recoilX = 0f;
    float recoilY = 1f;

    public float currentRecoil = 0;
    float maxRecoil = 0.4f;
    float addRecoil = 0.01f;
    float remRecoil = -0.03f;

    float lastFireTime = 0;
    float resetFireIntervalTime = 0.35f;

    public float intervalTime = 0.15f;
    float curTime = 0;

    void Update()
    {
        curTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && curTime > intervalTime)
        {
            curTime = 0;

            float currentfireIntervalTime = Time.time - lastFireTime;
            float intervalTime = currentfireIntervalTime - resetFireIntervalTime;
            if (intervalTime > 0)
            {
                currentRecoil -= intervalTime * -10 * remRecoil;
                if (currentRecoil < 0)
                {
                    currentRecoil = 0;
                }
            }
            else
            {
                currentRecoil += intervalTime * -10 * addRecoil;

                if (currentRecoil > maxRecoil)
                {
                    currentRecoil = maxRecoil;
                }
            }

            lastFireTime = Time.time;

            Vector3 position = transform.position;
            Vector3 direction = transform.forward + new Vector3(recoilX * currentRecoil, recoilY * currentRecoil, 0);

            var bullet = bulletPool.Allocate();
            bullet.Fire(position, direction, 3f, (bulletArg) => bulletPool.Release(bulletArg));

        }
    }

    private Bullet OnCreateBullet()
    {
        var _bulletGameObject = Instantiate(bulletGameObject);
        return _bulletGameObject.GetComponent<Bullet>();
    }
    private void OnDestoryBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

}
