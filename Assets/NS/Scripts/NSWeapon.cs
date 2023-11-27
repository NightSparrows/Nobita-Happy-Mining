using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NSWeapon 
{
    public bool activated = true;

    public virtual void init(Player player) { }
    public virtual void updateWeapon(float dt) { }

}
