using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUpgrade : Upgrade
{
    public DefaultUpgrade(Buff buff)
    {
        this.buff = buff;
    }

    public override string sourceName => "Default";

    protected override void NotifyHelper() { }
}
