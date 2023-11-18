using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffProviderHolder : MonoBehaviour
{
    [SerializeField] private Transform _container;
    private void Awake()
    {
        if (_container == null)
        {
            GameObject container = new GameObject("Buff Providers");
            _container = container.transform;
        }

        _container.parent = transform;
    }
    public void RecieveBuffProvider(BuffProvider newBuffProvider)
    {
        newBuffProvider.gameObject.transform.parent = _container.transform;
    }
}
