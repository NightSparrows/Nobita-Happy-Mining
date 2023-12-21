using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dungeon", menuName = "Level/Dungeon")]
public class Dungeon : ScriptableObject
{
    public GameObject[] levelPrefabs;
    public int initLevelIdx = 0;

    // (level index, teleporter index)
    public int[] escapeLevelIndex;
    public int[] escapeTeleporterIndex;

    public (int, int)[] teleporterLinkFrom;
    public (int, int)[] teleporterLinkTo;

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
    private void BindTeleporter(DungeonManager manager, GameObject[] levels)
    {
        BindEscapeTeleporters(manager, levels);
        BindTeleporterLinks(manager, levels);
    }

    private void BindEscapeTeleporters(DungeonManager manager, GameObject[] levels)
    {
        if (escapeLevelIndex.Length != escapeTeleporterIndex.Length)
        {
            Debug.Log("Link Error");
            return;
        }

        int n = levels.Length;
        for (int i = 0; i < escapeLevelIndex.Length; ++i)
        {
            int levelIdx = escapeLevelIndex[i];
            int teleIdx = escapeTeleporterIndex[i];

            if (levelIdx >= n)
            {
                Debug.Log("Link Error");
                return;
            }

            Level level = levels[levelIdx].GetComponent<Level>();
            if (level == null)
            {
                Debug.Log("Link Error");
                return;
            }

            if (teleIdx >= level.teleporters.Length)
            {
                Debug.Log("Link Error");
                return;
            }

            Teleporter teleporter = level.teleporters[teleIdx];
            if (teleporter == null)
            {
                Debug.Log("Link Error");
                return;
            }

            teleporter.OnTeleport += GameManager.Instance.OnPlayerEscape;
        }
    }

    private void BindTeleporterLinks(DungeonManager manager, GameObject[] levels)
    {
        // TODO: links between teleporters by teleporterLinkFrom and teleporterLinkTo
    }
}
