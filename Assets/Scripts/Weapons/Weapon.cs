using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action<GameObject> OnInstantiateBullet;

    public WeaponHolder holder { get; set; }


    protected void InvokeInstantiateBullet(GameObject bullet)
    {
        OnInstantiateBullet?.Invoke(bullet);
    }
}
