using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponHolder))]
public class WeaponTestPlayer : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject itemPrefab;

    private WeaponHolder weaponHolder;
    private ItemHolder itemHolder;

    private void Awake()
    {
        weaponHolder = GetComponent<WeaponHolder>();
        itemHolder = GetComponent<ItemHolder>();
    }

    private void Start()
    {
        GameObject weapon = Instantiate(weaponPrefab);
        weaponHolder.RecieveWeapon(weapon);

        GameObject item = Instantiate(itemPrefab);
        itemHolder.RecieveItem(item);
    }
}
