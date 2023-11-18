using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKeyCodeWeapon : Weapon
{
    [SerializeField] private KeyCode key = KeyCode.Space;
    [SerializeField] private GameObject bullet;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("Instantiate Bullet");
            Bullet b = Instantiate(bullet).GetComponent<Bullet>();
            HandleInstantiateBullet(b);
        }
    }
}
