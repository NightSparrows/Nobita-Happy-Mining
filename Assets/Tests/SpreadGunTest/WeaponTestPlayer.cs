using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponHolder))]
public class WeaponTestPlayer : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;

    private WeaponHolder holder;

    private void Awake()
    {
        holder = GetComponent<WeaponHolder>();
    }

    private void Start()
    {
        GameObject weapon = Instantiate(weaponPrefab);
        holder.RecieveWeapon(weapon);
    }
}
