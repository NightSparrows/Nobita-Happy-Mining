using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new WaveSO", menuName = "Wave/Basic Wave")]
public class WaveSO : ScriptableObject
{
    public GameObject enemyGenerator;
    public float duration;
    public float startTime;
    public void Activate(EnemyManger manager)
    {
        GameObject obj = Instantiate(enemyGenerator, manager.transform);
        SetupGenerator(manager, obj);
        Destroy(obj, duration);
    }

    protected virtual void SetupGenerator(EnemyManger manager, GameObject obj) { }
}
