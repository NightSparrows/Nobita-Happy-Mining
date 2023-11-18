using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private void Awake()
    {
        GameObject container = new GameObject("Weapons");
        container.transform.parent = transform;
        _container = container.transform;
    }

    public void GetWeapon(BaseWeapon newWeapon)
    {
        newWeapon.gameObject.transform.parent = _container.transform;
    }
}
