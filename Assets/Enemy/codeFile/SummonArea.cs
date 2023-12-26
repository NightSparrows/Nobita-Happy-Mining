using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonArea : MonoBehaviour
{
    [SerializeField] float summonTime = 1f;
    private GameObject creature;


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, appearTime);
        Invoke("Summon", summonTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Summon()
    {
        //Instantiate(creature, transform.position, Quaternion.identity);

        Vector3 pos = transform.position + new Vector3(0, 2f, 0);
        Instantiate(creature, pos, Quaternion.identity);

        Destroy(gameObject);
    }

    public void SetTime(float newTime)
    {
        summonTime = newTime;
    }

    public void SetCreature(GameObject newCreature)
    {
        creature = newCreature;
    }
}
