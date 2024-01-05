using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CircleWaveSO", menuName = "Wave/Circle Wave")]

public class CircleWaveSO : WaveSO
{
    public GameObject enemy;
    public GameObject spawnArea;
    public float coolDown;
    public int enemyNumber;
    public float radius;

    protected override void SetupGenerator(EnemyManger manager, GameObject obj)
    {
        CircleEnemyGenerator generator = obj.GetComponent<CircleEnemyGenerator>();
        generator.enemy = enemy;
        generator.coolDown = coolDown;
        generator.target = manager.player.transform;
        generator.spawnArea = spawnArea;
        generator.enemyNumber = enemyNumber;
        generator.radius = radius;
    }
}
