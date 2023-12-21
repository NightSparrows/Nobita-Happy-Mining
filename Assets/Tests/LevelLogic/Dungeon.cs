using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dungeon", menuName = "Level/Dungeon")]
public class Dungeon : ScriptableObject
{
    public GameObject[] levelPrefabs;
    public int initLevelIdx = 0;

    public GameObject[] Generate(DungeonManager manager)
    {
        GameObject[] levels = new GameObject[levelPrefabs.Length];

        for (int i = 0; i < levelPrefabs.Length; ++i)
        {
            levels[i] = Instantiate(levelPrefabs[i], manager.transform);
            levels[i].SetActive(i == initLevelIdx);
        }

        BindTeleporter(manager, levels);

        return levels;
    }

    // BindTeleporter is responsible to subscribe DungeonManager.OnLevelChanged
    //      for every teleporter switching level
    public virtual void BindTeleporter(DungeonManager manager, GameObject[] levels) { }
}
