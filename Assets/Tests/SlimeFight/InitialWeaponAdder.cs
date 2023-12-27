using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponHolder))]
public class InitialWeaponAdder : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private void Start()
    {
        WeaponHolder holder = GetComponent<WeaponHolder>();

        GameObject obj = Instantiate(weapon);
        holder.RecieveWeapon(obj);

        Destroy(this);
    }
}
