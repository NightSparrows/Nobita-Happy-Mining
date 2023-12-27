using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearUpgraderHelper : UpgraderHelper
{
    [field: SerializeField] public LinearUpgrader upgrader { get; set; }
    public string upgraderName;

    public int level { get; protected set; } = 0;

    public string sourceName => upgraderName;

    public override Upgrade[] GetAvailableUpgrades()
    {
        if (level == upgrader.maxLevel) return new LinearUpgrade[0];
        return new Upgrade[1] { new LinearUpgrade(level, upgrader.upgrades[level], this) };
    }

    public void Notified(LinearUpgrade upgrade)
    {
        level = upgrade.index + 1;
    }
}
