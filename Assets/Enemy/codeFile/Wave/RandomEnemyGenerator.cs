using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyGenerator : EnemyGenerator
{
    public float coolDown;
    public GameObject enemy;
    public GameObject spawnArea;
    public Transform target;
    public int enemyNumber;
    public float radius;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            for(int i = 0; i < enemyNumber; ++i)
            {
                Vector3 pos = target.position;
                // TODO: create enemy in random position
                //(1) random position
                //(2) check if can create in that position
                //(3) go back to (1) if can't

                //(4) create obj
                GameObject obj = Instantiate(spawnArea, pos, Quaternion.identity);
                obj.GetComponent<SummonArea>().SetCreature(enemy);
            }
            //GameObject obj = Instantiate(spawnArea, target.position, Quaternion.identity);
            //obj.GetComponent<SummonArea>().SetCreature(enemy);
            yield return new WaitForSeconds(coolDown);
        }

    }
}
