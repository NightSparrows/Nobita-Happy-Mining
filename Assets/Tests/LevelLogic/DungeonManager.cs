using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Dungeon dungeonSO;
    [SerializeField] private GameObject player;

    public event Action OnLevelChanged;

    public Level currentLevel
    {
        get
        {
            return levels[currentLevelIdx].GetComponent<Level>();
        }
    }

    private GameObject[] levels;
    private int currentLevelIdx;

    private void Start()
    {
        GenerateDungeon();
        OnLevelChanged += () => DestroyByTag("PlayerBullet");
        OnLevelChanged += () => DestroyByTag("Exp");
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
            //Restart();
        }
    }

    private void GenerateDungeon()
    {
        currentLevelIdx = dungeonSO.initLevelIdx;
        levels = dungeonSO.Generate(this);
        // TODO: set up player inital state
    }

    private void DestroyByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }
}
