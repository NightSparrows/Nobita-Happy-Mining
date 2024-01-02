using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyGenerator : EnemyGenerator
{
    public float coolDown;
    public GameObject enemy;
    public GameObject spawnArea;
    public Transform target;
    public int enemyNumber;
    public float radius;

    [SerializeField] LayerMask obstructionMask;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            // TODO: create enemy in circle
            for (int i = 0; i < enemyNumber; ++i)
            {
                Vector3 pos = target.position;

                float theta = (2f / enemyNumber) * i * Mathf.PI;
                pos += new Vector3(radius * Mathf.Cos(theta), 0, radius * Mathf.Sin(theta));
                RaycastHit hit;
                bool hasHitObstacle = Physics.Raycast(pos, Vector3.up, out hit, 3.0f, obstructionMask);
                if (hasHitObstacle)
                {
                    //continue;
                }

                //  create obj
                GameObject obj = Instantiate(spawnArea, pos, Quaternion.identity);
                obj.GetComponent<SummonArea>().SetCreature(enemy);
            }
            //GameObject obj = Instantiate(spawnArea, target.position, Quaternion.identity);
            //obj.GetComponent<SummonArea>().SetCreature(enemy);
            yield return new WaitForSeconds(coolDown);
        }

    }
}
