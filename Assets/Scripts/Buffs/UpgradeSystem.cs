using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeSystem", menuName = "Upgrade/Upgrade System")]
public class UpgradeSystem : ScriptableObject
{
    [SerializeField] protected Buff[] upgrades;

    public int level { get; protected set; } = 0;
    public int maxLevel
    {
        get
        {
            return upgrades.Length - 1;
        }
    }

    public void Upgrade(GameObject target)
    {
        if (level == maxLevel)
        {
            Debug.LogWarning("Upgrade a Fully upgraded object: " + target.name);
            return;
        }

        ApplyTo(upgrades[level], target);
        ++level;
    }

    protected virtual void ApplyTo(Buff buff, GameObject target)
    {
        buff.ApplyTo(target);
    }
}
