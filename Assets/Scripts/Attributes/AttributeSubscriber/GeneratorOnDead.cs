using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class GeneratorOnDead : MonoBehaviour
{
    public float prob = 1f;
    public GameObject obj;

    private void Awake()
    {
        Health health = GetComponent<Health>();
        health.OnDead += OnDead;
    }

    private void OnDead()
    {
        if (Random.value < prob)
        {
            Instantiate(obj, transform.position, transform.rotation);
        }
    }
}
