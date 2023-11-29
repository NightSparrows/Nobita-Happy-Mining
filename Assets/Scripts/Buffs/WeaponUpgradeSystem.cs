using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponUpgradeSystem", menuName = "Upgrade/Weapon Upgrade System")]
public class WeaponUpgradeSystem : UpgradeSystem
{
    protected override void ApplyTo(Buff buff, GameObject target)
    {
        Weapon weapon = target.GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.LogWarning("Weapon Upgrade to a Non-weapon:" + target.name);
            return;
        }
        WeaponHolder holder = weapon.holder;
        buff.ApplyTo(holder.gameObject);
    }
}
