using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject player;


    /*
     * ---- Singleton Pattern of GameManager ----
     * 
     *      Ref: https://bergstrand-niklas.medium.com/setting-up-a-simple-game-manager-in-unity-24b080e9516c
     *      
     *      Q: How to Access GameManager?
     *      A: E.g. `GameManager.Instance.level`
     *      
     * ----                                  ----
     */
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager instance is missing");
                return null;
            }
                
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    /*
     * ---- Finite State Machine of GameState and Events ----
     * 
     *      Ref: https://www.youtube.com/watch?v=4I0vonyqMi8
     *      
     *      Q: How to Attatch OnGameStateChanged event?
     *      A: `GameManager.OnGameStateChanged += YourCallback;` in Awake()
     *         `GameManager.OnGameStateChanged -= YourCallback;` in OnDestroy()
     *         
     * ----                                  ----
     */
    public static event Action<GameState> OnGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Playing:
                break;
            case GameState.Upgrading:
                break;
            case GameState.Victory:
                break;
            case GameState.Dead:
                break;
            case GameState.Timeout:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, "Unknown Game State");
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void Start()
    {
        UpdateGameState(GameState.Playing);
    }
}

public enum GameState
{
    Playing,
    Upgrading,
    Victory,
    Dead,
    Timeout
}   