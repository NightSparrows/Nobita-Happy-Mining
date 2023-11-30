using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LinearUpgrader", menuName = "Upgrade/Linear Upgrader")]
public class LinearUpgrader : Upgrader
{
    public Buff[] upgrades;

    public int maxLevel
    {
        get
        {
            return upgrades.Length;
        }
    }

    public override void Upgrade(object indicator, GameObject target)
    {
        int? level = indicator as int?;
        int realLevel = level ?? default(int);
        if (level == maxLevel)
        {
            Debug.LogWarning("Upgrade a Fully upgraded object: " + target.name);
            return;
        }

        upgrades[realLevel].ApplyTo(target);
    }

    //protected virtual void ApplyTo(Buff buff, GameObject target)
    //{
    //    buff.ApplyTo(target);
    //}


}