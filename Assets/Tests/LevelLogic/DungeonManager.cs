using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DungeonManager : MonoBehaviour
{
    public Dungeon dungeonSO;
    [SerializeField] private GameObject player;

    // <Level from, Level to, teleporter from, teleporter to>
    public event Action<int, int, Teleporter, Teleporter> OnTeleportToLevel;

    public Level currentLevel
    {
        get
        {
            return levels[currentLevelIdx].GetComponent<Level>();
        }
    }

    private GameObject[] levels;
    private int currentLevelIdx;

    public Teleporter GetTeleporterFrom(int teleporterIndex)
    {
        int level = dungeonSO.teleporterLinkLevelFrom[teleporterIndex];
        int teleporter = dungeonSO.teleporterLinkFrom[teleporterIndex];
        return levels[level].GetComponent<Level>().teleporters[teleporter];
    }

    public Teleporter GetTeleporterTo(int teleporterIndex)
    {
        int level = dungeonSO.teleporterLinkLevelTo[teleporterIndex];
        int teleporter = dungeonSO.teleporterLinkTo[teleporterIndex];
        return levels[level].GetComponent<Level>().teleporters[teleporter];
    }

    private void Start()
    {
        GenerateDungeon();
        //OnLevelChanged += () => DestroyByTag("PlayerBullet");
        //OnLevelChanged += () => DestroyByTag("Exp");
    }

    public void Teleport(int newLevelIdx, Transform teleporter)
    {
        if (newLevelIdx != currentLevelIdx)
        {
            SaveCurrent();
            levels[currentLevelIdx].SetActive(false);
            levels[newLevelIdx].SetActive(true);
            currentLevelIdx = newLevelIdx;
        }

        if (newLevelIdx == 2)
        {
            // boss level
            Debug.Log("Enter boss level!");
            var mng = levels[newLevelIdx].transform.Find("Managers/LevelManager").GetComponent<NSLevelGameManager>();
            mng.player = player;
            mng.m_camera = GameObject.Find("GameCamera").GetComponent<GameCamera>();
            NSLevelGameManager.StartLevel();
        }

        //CharacterController cc = player.GetComponent<CharacterController>();
        //cc.Move(teleporter.position - player.transform.position);
        player.transform.position = teleporter.position;
        player.transform.rotation = teleporter.rotation;
    }

    public void OnTeleporterCallback(int levelFrom, int levelTo, Teleporter teleporterFrom, Teleporter teleporterTo)
    {
        OnTeleportToLevel?.Invoke(levelFrom, levelTo, teleporterFrom, teleporterTo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //Restart();
        }
    }

    private void SaveCurrent()
    {
        Transform container = currentLevel.container;
        SetParentByTag("Exp", container);
        SetParentByTag("Enemy", container);
    }

    private void GenerateDungeon()
    {
        currentLevelIdx = dungeonSO.initLevelIdx;
        levels = dungeonSO.Generate(this);
        // TODO: set up player inital state
    }

    private void SetParentByTag(string tag, Transform parent)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objs)
        {
            obj.transform.parent = parent;
        }
    }
}
