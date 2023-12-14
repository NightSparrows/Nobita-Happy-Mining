using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHolder : MonoBehaviour
{
    public event Action<GameObject> OnRecievewItem, OnDiscardItem;

    [SerializeField] private Transform _container;

    public GameObject[] itemList
    {
        get
        {
            int n = _container.childCount;
            GameObject[] objs = new GameObject[n];
            for (int i = 0; i < n; ++i)
            {
                objs[i] = _container.GetChild(i).gameObject;
            }
            return objs;
        }
    }

    private void Awake()
    {
        if (_container == null)
        {
            GameObject container = new GameObject("Items");
            _container = container.transform;
        }

        _container.parent = transform;
    }

    public void RecieveItem(GameObject newItem)
    {
        newItem.transform.parent = _container.transform;
        newItem.GetComponent<Item>().holder = this;
        OnRecievewItem?.Invoke(newItem);
    }

    public void DiscardItem(GameObject item)
    {
        OnDiscardItem?.Invoke(item);
        Destroy(item.gameObject);
    }
}
