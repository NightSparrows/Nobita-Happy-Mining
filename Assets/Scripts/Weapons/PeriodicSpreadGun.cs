using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletNumber))]
[RequireComponent(typeof(BulletSpread))]
public class PeriodicSpreadGun : PeriodicGun
{
    [SerializeField] protected GameObject bulletPrefab;

    protected BulletNumber bulletNumber;
    protected BulletSpread bulletSpread;

    protected override void Awake()
    {
        base.Awake();
        bulletNumber = GetComponent<BulletNumber>();
        bulletSpread = GetComponent<BulletSpread>();
    }

    protected override void Shoot()
    {
        int n = bulletNumber.value;

        if (n == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            InvokeInstantiateBullet(bullet);
            return;
        }

        float allAngle = bulletSpread.value;
        float minAngle = -allAngle / 2;
        float stepAngle = allAngle / (n - 1);
        for (int i = 0; i < n; ++i)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            float curAngle = minAngle + i * stepAngle;
            bullet.transform.Rotate(0f, curAngle, 0f);
            InvokeInstantiateBullet(bullet);
        }
    }
}
