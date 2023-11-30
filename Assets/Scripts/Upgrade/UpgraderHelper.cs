using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgraderHelper : MonoBehaviour
{
    public abstract (Buff, object)[] GetAvailableUpgrades();

    public abstract void Upgrade(object indicator, GameObject target);
}
