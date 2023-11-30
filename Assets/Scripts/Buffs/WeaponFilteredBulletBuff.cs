using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFilteredBulletBuff<T> : BulletBuff
{

    protected override bool WeaponFilter(GameObject weapon)
    {
        return weapon.GetComponent<T>() != null;
    }
}
