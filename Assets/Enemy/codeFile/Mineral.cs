using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private Health health;


    private void Awake()
    {
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {

        health.OnDead += HandleDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleDeath()
    {
        Debug.Log("Mineral.HandleDestroy");
        SelfDestroy();
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        health.OnDead -= HandleDeath;
    }
}
