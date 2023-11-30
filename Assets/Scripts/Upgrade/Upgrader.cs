using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrader : ScriptableObject
{

    public abstract void Upgrade(object indicator, GameObject target);
    //public abstract void Upgrade(GameObject target);
}
