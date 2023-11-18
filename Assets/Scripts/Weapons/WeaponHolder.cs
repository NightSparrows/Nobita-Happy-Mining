using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public event Action<Weapon> OnGetNewWeapon;

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
        OnGetNewWeapon?.Invoke(newWeapon);
    }
}
