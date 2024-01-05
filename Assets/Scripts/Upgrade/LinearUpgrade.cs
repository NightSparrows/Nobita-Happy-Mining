using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearUpgrade : Upgrade
{
    // index of the linear upgrade
    public int index;
    LinearUpgraderHelper source;

    public LinearUpgrade(int index, Buff buff, LinearUpgraderHelper source)
    {
        this.buff = buff;
        this.source = source;
        this.index = index;
    }

    public override string sourceName => source.upgraderName;

    protected override void NotifyHelper()
    {
        source.Notified(this);
    }
}
