using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoBullet : Bullet
{
    [SerializeField] Attack attack;
    [SerializeField] KeyCode key = KeyCode.Q;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("Attack is " + attack.BaseDamage);
        }
    }
}
