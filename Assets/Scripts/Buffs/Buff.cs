using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public abstract void ApplyTo(GameObject target);
    public virtual void RemoveFrom(GameObject target) { }

    public virtual string description { get { return string.Empty; } }
}
