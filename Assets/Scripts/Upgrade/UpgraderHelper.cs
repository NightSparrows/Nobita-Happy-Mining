using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgraderHelper : MonoBehaviour
{
    public abstract Upgrade[] GetAvailableUpgrades();
}
