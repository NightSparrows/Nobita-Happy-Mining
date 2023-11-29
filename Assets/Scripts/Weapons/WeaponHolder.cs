using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public event Action<Weapon> OnRecievewWeapon, OnDiscardWeapon;

    [SerializeField] private Transform _container;

    public Weapon[] WeaponList
    {
        get
        {
            return _container.GetComponentsInChildren<Weapon>();
        }
    }

    private void Awake()
    {
        if (_container == null)
        {
            GameObject container = new GameObject("Weapons");
            _container = container.transform;
        }

        _container.parent = transform;
    }

    public void RecieveWeapon(Weapon newWeapon)
    {
        newWeapon.gameObject.transform.parent = _container.transform;
        newWeapon.holder = this;
        OnRecievewWeapon?.Invoke(newWeapon);
    }

    public void DiscardWeapon(Weapon weapon)
    {
        OnDiscardWeapon?.Invoke(weapon);
        Destroy(weapon.gameObject);
    }
}
