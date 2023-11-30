using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponDiscarderBuff", menuName = "Buff/Weapon Discarder")]
public class WeaponDiscarderBuff : Buff
{
    public string weaponName;

    public override void ApplyTo(GameObject target)
    {
        WeaponHolder holder = target.GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("WeaponRecieverBuff is applied to Non-WeaponHolder object: " + target.name);
            return;
        }

        foreach (var weapon in holder.WeaponList)
        {
            if (weapon.gameObject.name == weaponName)
            {
                holder.DiscardWeapon(weapon);
                return;
            }
        }
    }
}
