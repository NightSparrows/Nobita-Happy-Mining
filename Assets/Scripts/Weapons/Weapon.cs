using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action<Bullet> OnInstantiateBullet;

    protected void HandleInstantiateBullet(Bullet bullet)
    {
        OnInstantiateBullet?.Invoke(bullet);
    }
}
