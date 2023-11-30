using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponRecieverBuff", menuName = "Buff/Weapon Reciever")]
public class WeaponRecieverBuff : Buff
{
    public GameObject weaponPrefab;

    public override void ApplyTo(GameObject target)
    {
        WeaponHolder holder = target.GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("WeaponRecieverBuff is applied to Non-WeaponHolder object: " + target.name);
            return;
        }

        GameObject obj = Instantiate(weaponPrefab);
        holder.RecieveWeapon(obj);
    }
}
