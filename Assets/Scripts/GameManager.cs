using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public DungeonManager dungeonManager;

    [field : SerializeField] public GameState state { get; private set; } = GameState.Playing;

    // Time
    public bool isPaused { get; private set; } = false;
    private float timeScale;

    // Teleporter
    private int levelAfterShop;
    private Transform teleportAfterShop;

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

    public static bool DoesInstanceExit() => _instance != null;

    private void Awake()
    {
        if (_instance)
        {
            Debug.Log("instance exist, destroy self");
            Destroy(gameObject);
        }
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("Subscribe on scene loaded");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion

    /*
     * ---- Finite State Machine of GameState and Events ----
     * 
     *      Ref: https://www.youtube.com/watch?v=4I0vonyqMi8
     *      
     *      Q: How to Attatch OnGameStateChanged event?
     *      A: `GameManager.Instance.OnGameStateChanged += YourCallback;` in OnEnable()
     *         `GameManager.Instance.OnGameStateChanged -= YourCallback;` in OnDisable()
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
        Debug.Log("update game state " + newState.ToString());

        OnGameStateExit?.Invoke(state, newState);
        var prevState = state;
        state = newState;
        OnGameStateEnter?.Invoke(prevState, state);
    }

    void Start()
    {
        //UpdateGameState(GameState.Playing);
        //FindPlayer();
        //SubscribePlayerEvents();
        //SubscribeDungeonEvent();
    }

    public void PauseTime()
    {
        if (isPaused) return;
        isPaused = true;
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = timeScale;
    }

    public void Restart()
    {
        SceneLoadingManager.Instance.LoadGameScene();
        //SceneManager.LoadScene("LevelLogicScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z");
            Application.Quit();
        }
    }

    /*
     * Subscribe from `SceneManager.sceneLoaded += OnSceneLoaded;`
     *      - called after a sscene is fully loaded
     *      - after intial GameObject Awake()
     *      - before Start()
     */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("on scene loaded " + scene.name);
        state = GameState.Playing;
        //FindPlayer();
        SubscribePlayerEvents();

        //FindDungeonManager();
        SubscribeDungeonEvent();

        if (isPaused)
        {
            ResumeTime();
        }
    }

    private void FindPlayer()
    {
        if (player != null)
        {
            Debug.Log("player still!");
        }
        player = GameObject.FindWithTag("Player");
    }

    private void SubscribePlayerEvents()
    {
        Debug.Log("subscribe");
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.OnDead += OnPlayerDead;

        Stamina playerStamina = player.GetComponent<Stamina>();
        playerStamina.OnStaminaChanged += OnPlayerStaminaChanged;

        PlayerExperience playerExp = player.GetComponent<PlayerExperience>();
        playerExp.OnPlayerLevelChanged += OnPlayerLevelChanged;

        UpgradeManager playerUpgrade = player.GetComponent<UpgradeManager>();
        playerUpgrade.OnUpgradeEnd += OnPlayerUpgradeEnd;

        ShopConsumer playerConsumer = player.GetComponent<ShopConsumer>();
        playerConsumer.OnShoppingEnd += OnPlayerShoppingEnd;
    }

    private void FindDungeonManager()
    {
        dungeonManager = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
    }

    private void SubscribeDungeonEvent()
    {
        dungeonManager.OnTeleportToLevel += OnTeleportToLevel;
    }

    private void OnPlayerDead()
    {
        UpdateGameState(GameState.Dead);
    }

    private void OnPlayerStaminaChanged(int value)
    {
        if (value <= 0)
        {
            // player timeout
            UpdateGameState(GameState.Timeout);
        }
    }

    private void OnPlayerLevelChanged(int level)
    {
        UpdateGameState(GameState.Upgrading);
    }

    private void OnPlayerUpgradeEnd()
    {
        UpdateGameState(GameState.Playing);
    }

    
    private void OnPlayerShoppingEnd()
    {
        // telepport to the level by 
        //      private int levelAfterShop;
        //      private Transform teleportAfterShop;
        dungeonManager.Teleport(levelAfterShop, teleportAfterShop);
        UpdateGameState(GameState.Playing);
    }

    private void OnTeleportToLevel(int levelFrom, int levelTo, Teleporter teleporterFrom, Teleporter teleporterTo)
    {
        if (teleporterFrom.teleportCounter == 1 && teleporterTo.teleportCounter == 0)
        {
            // first time use this link
            // go into shop first, then teleport to the new level
            levelAfterShop = levelTo;
            teleportAfterShop = teleporterTo.transform;
            UpdateGameState(GameState.Shopping);
        }
        else
        {
            // this link has been used before
            // teleport to the new level directly
            dungeonManager.Teleport(levelTo, teleporterTo.transform);
            UpdateGameState(GameState.Playing);
        }
    }

    // Called by Teleporter
    public void OnPlayerEscape()
    {
        UpdateGameState(GameState.Victory);
    }
}

public enum GameState
{
    Playing,
    Upgrading,
    Victory,
    Dead,
    Timeout,
    Shopping
}   