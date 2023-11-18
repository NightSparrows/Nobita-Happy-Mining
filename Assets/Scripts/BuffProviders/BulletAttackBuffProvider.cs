using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackBuffProvider : BuffProvider
{
    [SerializeField] private AttackBuff buff;
    public void Activate(WeaponHolder holder)
    {
        if (IsInvalid) return;

        foreach (var weapon in holder.WeaponList)
        {
            AttachBulletBuffer(weapon);
        }
        holder.OnGetNewWeapon += AttachBulletBuffer;
    }

    private void AttachBulletBuffer(Weapon newWeapon)
    {
        if (IsInvalid) return;
        
        newWeapon.OnInstantiateBullet += BuffBullet;
    }

    private void BuffBullet(Bullet bullet)
    {
        if (IsInvalid) return;

        buff.BuffTo(bullet.gameObject.GetComponent<Attack>());
    }
}
