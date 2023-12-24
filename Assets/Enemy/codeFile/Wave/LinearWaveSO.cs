using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LinearWaveSO", menuName = "Wave/Linear Wave")]
public class LinearWaveSO : WaveSO
{
    public GameObject enemy;
    public GameObject spawnArea;
    public float coolDown;

    protected override void SetupGenerator(EnemyManger manager, GameObject obj)
    {
        LinearEnemyGenerator generator = obj.GetComponent<LinearEnemyGenerator>();
        generator.enemy = enemy;
        generator.coolDown = coolDown;
        generator.target = manager.player.transform;
        generator.spawnArea = spawnArea;
    }
}
