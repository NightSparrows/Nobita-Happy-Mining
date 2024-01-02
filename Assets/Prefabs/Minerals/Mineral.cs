using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    private Health health;

    [SerializeField] float expDropRate;
    [SerializeField] float rubyDropRate;
    [SerializeField] float diamondDropRate;

    [SerializeField] GameObject expPickUp;
    [SerializeField] GameObject rubyPickUp;
    [SerializeField] GameObject diamondPickUp;


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
        DropPickUp();
        Destroy(gameObject);
    }

    private void DropPickUp()
    {
        // drop exp
        float r = Random.Range(0.0f, 1.0f);
        if (r >= expDropRate)
        {
            Instantiate(expPickUp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        }

        // drop ruby
        r = Random.Range(0.0f, 1.0f);
        if (r >= rubyDropRate)
        {
            Instantiate(rubyPickUp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        }

        // drop diamond
        r = Random.Range(0.0f, 1.0f);
        if (r >= diamondDropRate)
        {
            Instantiate(diamondPickUp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        health.OnDead -= HandleDeath;
    }
}
