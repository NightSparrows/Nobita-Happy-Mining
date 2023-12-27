using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    public Buff buff;

    public virtual void Activate(UpgradeManager manager)
    {
        buff.ApplyTo(manager.gameObject);
        NotifyHelper();
    }

    // notify the upgrader helper, so that it can update level or sth
    protected abstract void NotifyHelper();
    public abstract string sourceName { get; }
}
