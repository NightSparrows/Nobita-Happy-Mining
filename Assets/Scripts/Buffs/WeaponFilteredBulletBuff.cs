using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFilteredBulletBuff<T> : BulletBuff
{

    protected override bool WeaponFilter(Weapon weapon)
    {
        return weapon is T;
    }
}
