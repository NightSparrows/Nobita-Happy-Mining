using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new NormalWaveSO", menuName = "Wave/Normal Wave")]

public class NormalWaveSO : WaveSO
{
    public GameObject enemy;
    public GameObject spawnArea;
    public float coolDown;
    public int enemyNumber;
    public float minRadius;
    public float maxRadius;

    protected override void SetupGenerator(EnemyManger manager, GameObject obj)
    {
        NormalEnemyGenerator generator = obj.GetComponent<NormalEnemyGenerator>();
        generator.enemy = enemy;
        generator.coolDown = coolDown;
        generator.target = manager.player.transform;
        generator.spawnArea = spawnArea;
        generator.enemyNumber = enemyNumber;
        generator.minRadius = minRadius;
        generator.maxRadius = maxRadius;
    }
}
