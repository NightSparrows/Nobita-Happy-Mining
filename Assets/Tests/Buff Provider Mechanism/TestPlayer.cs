using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] KeyCode key = KeyCode.W;
    [SerializeField] BulletAttackBuffProvider buffProvider;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("Buff Activate");
            buffProvider.Activate(GetComponent<WeaponHolder>());
        }    
    }
}
