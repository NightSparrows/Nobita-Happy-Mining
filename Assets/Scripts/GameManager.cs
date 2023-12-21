using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public DungeonManager dungeonManager;

    public GameState state { get; private set; } = GameState.Playing;

    public bool isPaused { get; private set; } = false;
    private float timeScale;

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
        FindPlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    public event Action<GameState, GameState> OnGameStateEnter;
    public event Action<GameState, GameState> OnGameStateExit;

    public void UpdateGameState(GameState newState)
    {
        /*switch (newState)
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
        }*/

        OnGameStateExit?.Invoke(state, newState);
        var prevState = state;
        state = newState;
        OnGameStateEnter?.Invoke(prevState, state);
    }

    void Start()
    {
        //UpdateGameState(GameState.Playing);
    }

    public void PauseTime()
    {
        isPaused = true;
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        isPaused = false;
        Time.timeScale = timeScale;
    }

    public void Restart()
    {
        // TODO: load scene
        SceneManager.LoadScene("LevelLogicScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
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