using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public event Action<GameObject> OnRecievewWeapon, OnDiscardWeapon;

    [SerializeField] private Transform _container;

    public GameObject[] WeaponList
    {
        get
        {
            Weapon[] children = _container.GetComponentsInChildren<Weapon>(true);
            int n = children.Length;
            GameObject[] objs = new GameObject[n];
            for (int i = 0; i < n; ++i)
            {
                objs[i] = children[i].gameObject;
                //Debug.Log("weapon! " + objs[i].name);
            }
            return objs;
            /*int n = _container.childCount;
            GameObject[] objs = new GameObject[n];
            for (int i = 0; i < n; ++i)
            {
                objs[i] = _container.GetChild(i).gameObject;
            }
            return objs;*/
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

    public void RecieveWeapon(GameObject newWeapon)
    {
        newWeapon.transform.parent = _container.transform;
        newWeapon.GetComponent<Weapon>().holder = this;
        OnRecievewWeapon?.Invoke(newWeapon);
    }

    public void DiscardWeapon(GameObject weapon)
    {
        OnDiscardWeapon?.Invoke(weapon);
        Destroy(weapon.gameObject);
    }
}
