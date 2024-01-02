using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearEnemyGenerator : EnemyGenerator
{
    public float coolDown;
    public GameObject enemy;
    public GameObject spawnArea;
    public Transform target;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            GameObject obj = Instantiate(spawnArea, target.position, Quaternion.identity);
            obj.GetComponent<SummonArea>().SetCreature(enemy);
            yield return new WaitForSeconds(coolDown);
        }
        
    }
}
