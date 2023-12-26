using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponBuff", menuName = "Buff/Weapon Buff")]
public class WeaponBuff : Buff
{
    [field: SerializeField] public Buff buff { get; set; }

    protected virtual bool WeaponFilter(GameObject weapon) => true;

    public override string description
    {
        get
        {
            //Debug.Log("BulletBuff GenerateDescription");
            return buff.description;
        }
    }

    public override void ApplyTo(GameObject target)
    {
        WeaponHolder holder = target.GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("WeaponBuff is applied to Non-WeaponHolder object:" + target.name);
            return;
        }
        Debug.Log("holder name " + target.name);

        foreach (var weapon in holder.WeaponList)
        {
            //Debug.Log("weapon " + weapon.name);
            AttachBulletBuff(weapon);
        }
    }

    private void AttachBulletBuff(GameObject weapon)
    {
        if (WeaponFilter(weapon))
            buff.ApplyTo(weapon);
    }
}
