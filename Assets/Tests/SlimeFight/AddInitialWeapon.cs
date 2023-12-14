using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInitialWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private void Start()
    {
        WeaponHolder holder = GetComponent<WeaponHolder>();
        if (holder == null)
        {
            Debug.Log("WeaponHolder not found");
            return;
        }

        GameObject obj = Instantiate(weapon);
        holder.RecieveWeapon(obj);

        Destroy(this);
    }
}
