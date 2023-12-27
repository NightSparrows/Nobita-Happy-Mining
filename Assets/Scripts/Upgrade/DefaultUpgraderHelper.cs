using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUpgraderHelper : UpgraderHelper
{
    [SerializeField] Buff[] buffs;

    public override Upgrade[] GetAvailableUpgrades()
    {
        int n = buffs.Length;
        DefaultUpgrade[] upgrades = new DefaultUpgrade[n];
        for (int i = 0; i < n; ++i)
        {
            upgrades[i] = new DefaultUpgrade(buffs[i]);
        }
        return upgrades;
    }
}
