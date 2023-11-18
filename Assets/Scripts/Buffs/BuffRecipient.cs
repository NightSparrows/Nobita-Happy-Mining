using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffRecipient : MonoBehaviour
{
    private Transform _container;
    private void Awake()
    {
        GameObject container = new GameObject("Buffs");
        container.transform.parent = transform;
        _container = container.transform;
    }
    public void RecieveBuff(BaseBuff newBuff)
    {
        newBuff.gameObject.transform.parent = _container.transform;
    }
}
