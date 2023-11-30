using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearUpgraderHelper : UpgraderHelper
{
    [field: SerializeField] public LinearUpgrader upgrader { get; set; }

    public int level { get; protected set; } = 0;

    public override (Buff, object)[] GetAvailableUpgrades()
    {
        if (level == upgrader.maxLevel) return new (Buff, object)[0];
        return new (Buff, object)[1] { (upgrader.upgrades[level], level) };
    }

    public override void Upgrade(object indicator, GameObject target)
    {
        upgrader.Upgrade(indicator, target);
        ++level;
    }

}
