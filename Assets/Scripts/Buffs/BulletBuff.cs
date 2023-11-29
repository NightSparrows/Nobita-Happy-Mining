using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulletBuff", menuName = "Buff/Bullet Buff")]
public class BulletBuff : Buff
{
    public Buff buff { get; set; }

    protected virtual bool WeaponFilter(Weapon weapon) => true;
    protected virtual bool BulletFilter(GameObject bullet) => true;

    public override void ApplyTo(GameObject target)
    {
        WeaponHolder holder = target.GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("BulletBuff is applied to Non-WeaponHolder object:" + target.name);
            return;
        }

        foreach (var weapon in holder.WeaponList)
        {
            if (WeaponFilter(weapon))
                AttachBulletBuff(weapon);
        }
        holder.OnRecievewWeapon += AttachBulletBuff;
        holder.OnDiscardWeapon += DeattachBulletBuff;
    }

    public override void RemoveFrom(GameObject target)
    {
        WeaponHolder holder = target.GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("BulletBuff is removed from Non-WeaponHolder object");
            return;
        }

        foreach (var weapon in holder.WeaponList)
        {
            if (WeaponFilter(weapon))
                DeattachBulletBuff(weapon);
        }
        holder.OnRecievewWeapon -= AttachBulletBuff;
        holder.OnDiscardWeapon -= DeattachBulletBuff;
    }

    private void AttachBulletBuff(Weapon newWeapon)
    {
        if (WeaponFilter(newWeapon))
            newWeapon.OnInstantiateBullet += BuffBullet;
    }

    private void DeattachBulletBuff(Weapon weapon)
    {
        if (WeaponFilter(weapon))
            weapon.OnInstantiateBullet -= BuffBullet;
    }

    private void BuffBullet(GameObject bullet)
    {
        if (BulletFilter(bullet))
            buff.ApplyTo(bullet);
    }
}
