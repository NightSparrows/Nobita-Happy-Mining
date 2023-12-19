using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Dungeon dungeonSO;
    [SerializeField] private GameObject player;

    private GameObject[] levels;
    private int currentLevelIdx;

    private void Start()
    {
        currentLevelIdx = dungeonSO.initLevelIdx;
        levels = dungeonSO.Generate(this);
    }

    private void Restart()
    {
        foreach (var lvl in levels)
        {
            Destroy(lvl);
        }
        levels = dungeonSO.Generate(this);
        // TODO: restart player
    }

    private void Teleport(int newLevelIdx, Teleporter teleporter)
    {
        if (newLevelIdx != currentLevelIdx)
        {
            levels[currentLevelIdx].SetActive(false);
            levels[newLevelIdx].SetActive(true);
            currentLevelIdx = newLevelIdx;
        }
        player.transform.position = teleporter.transform.position;
        player.transform.rotation = teleporter.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Restart();
        }
    }
}
