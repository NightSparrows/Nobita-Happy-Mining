using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    public void ShootBullet()
    {
        var player = GameManager.Instance.player;

        Vector3 p = player.transform.position;
        p.y = 0;
        // shoot bullet
        GameObject mBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        mBullet.transform.LookAt(p);
    }

}
