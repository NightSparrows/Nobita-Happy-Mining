using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Because this level can't be dulipcate so just use static method
public class NSLevelGameManager : MonoBehaviour
{
    public enum LevelState
    {
        None,                    // Do nothing state
        Init,                    // to initialize all the component of this level
        StartingDirector,        // the starting animation state
        WaitToGame,              // Wait a few time to prepare to enter game
        EnterGame,               // initialization for game start
        InGame,                  // in game
        Victory
    }

	// objects
	[SerializeField] public GameCamera m_camera;                                 // the main camera in scene
	[SerializeField] public GameObject player;
    [SerializeField] private GameObject nsBoss;
    [SerializeField] public GameObject m_backPortal;
    [SerializeField] public NSLevelEnemyManager m_enemyManager;

    [SerializeField] private AudioClip bossBGM;

	// some manager or controller
	private NSLevelStartDirector m_levelStartDirector;          // the director of the starting animation
    // startup fx
    [SerializeField] private GameObject m_startupFX;

	private Player m_player;
    private NSBossBehaviorScript m_bossBehavior;
    private AudioSource m_bgmPlayer;
    private Teleporter m_backPortalTeleporter;

    // variables
    public static NSLevelGameManager s_Instance;
    public LevelState m_state = LevelState.Init;

    public event Action<LevelState> OnLevelStateChanged;

    // state info

	private void Awake()
	{
		s_Instance = this;
        this.m_levelStartDirector = new NSLevelStartDirector();
        this.m_bgmPlayer = this.GetComponent<AudioSource>();
		// test 
		//StartLevel();
        // end test
        this.m_backPortal.SetActive(false);
        this.m_backPortalTeleporter = this.m_backPortal.GetComponent<Teleporter>();
	}

    public static void StartLevel()
	{
		s_Instance.updateLevelState(LevelState.Init);
		//s_Instance.updateLevelState(LevelState.StartingDirector);
	}

    protected void updateLevelState(LevelState newState)
    {
        // I dont know if the exit state api needed to be implement

        this.m_state = newState;

        switch(newState) { 
            case LevelState.Init:
                {
                    this.m_enemyManager.init();
				}
                break;
            case LevelState.StartingDirector:
				{
					this.player.transform.position = new Vector3(50, 0, 80);
					this.nsBoss.transform.position = new Vector3(50, 0, 50);
					this.player.transform.localScale = Vector3.one;

					this.m_bossBehavior = this.nsBoss.GetComponent<NSBossBehaviorScript>();
					this.m_bossBehavior.m_state = NSBossBehaviorScript.BossState.None;

					this.m_player = this.player.GetComponent<Player>();
                    this.m_player.canMove = false;

                    this.m_bossBehavior.m_player = this.m_player;
					// start the film initialize the camera position
					var laughMotion = new NSBossLaughMotion();
					laughMotion.gameCamera = this.m_camera;
					laughMotion.nsBossGO = this.nsBoss;
                    this.m_levelStartDirector.addMotion(laughMotion);

                    var jumpMotion = new NSBossJumpMotion();
                    jumpMotion.gameCamera = this.m_camera;
                    jumpMotion.nsBossGO = this.nsBoss;
					this.m_levelStartDirector.addMotion(jumpMotion);

                    var startupMotion = new NSBossVehicleStartupMotion();
					startupMotion.gameCamera = this.m_camera;
					startupMotion.nsBossGO = this.nsBoss;
                    startupMotion.startupFX = this.m_startupFX;
					this.m_levelStartDirector.addMotion(startupMotion);


					this.m_levelStartDirector.initDirector();
                    this.m_bgmPlayer.clip = this.bossBGM;
                    //this.m_bgmPlayer.pitch = 1.0833333333f;
                    this.m_bgmPlayer.Play();
                }
                break;
            case LevelState.WaitToGame:
				{
					this.m_bossBehavior.m_state = NSBossBehaviorScript.BossState.None;

					// a smooth trans to player camera
					this.m_camera.setViewType(GameCamera.ViewType.Linear);
                    this.m_camera.followSpeed = 25f;
					this.m_camera.setTarget(this.player.GetComponent<Player>().playerCamera.getTransform());
				}
                break;
            case LevelState.EnterGame:
				{
					this.m_bossBehavior.m_state = NSBossBehaviorScript.BossState.None;
					this.m_camera.setViewType(GameCamera.ViewType.Immediate);
					this.m_camera.followSpeed = 2f;
				}
                break;

			case LevelState.InGame:
                {
                    Debug.Log("Enter in game state");
					// start the boss battle ai
					this.m_bossBehavior.init();
					// init the game and start battle
					this.m_player.canMove = true;
					this.m_enemyManager.startSpawn();
				}
                break;
            case LevelState.Victory:
				{
					this.m_enemyManager.stopSpawn();
					this.m_bossBehavior.changeBossState(NSBossBehaviorScript.BossState.Death);
                    this.m_backPortalTeleporter.OnTeleport += () => {
                        this.player.transform.localScale = new Vector3(3, 3, 3);
                    };
                    this.m_bgmPlayer.Stop();
					this.m_backPortal.SetActive(true);
				}
                break;
                default: break;
        }

        OnLevelStateChanged?.Invoke(newState);
    } 

	// Start is called before the first frame update
	void Start()
	{
	}

    // Update is called once per frame
    void Update()
    {
        switch(this.m_state)
        {
            case LevelState.Init:
                {
					this.updateLevelState(LevelState.StartingDirector);
				}
                break;
            case LevelState.StartingDirector:
                {
                    if (this.m_levelStartDirector.update(Time.deltaTime))
                    {
                        // switch to in game state
                        this.updateLevelState(LevelState.WaitToGame);
                    }
                    this.m_player.Stamina.CurrentStamina = this.m_player.Stamina.MaxStamina;
                }
                break;
            case LevelState.WaitToGame:
				{
					if (this.m_camera.transform.position == this.m_camera.targetTransform.position)
					{
						this.updateLevelState(LevelState.EnterGame);
					}
                }break;
			case LevelState.EnterGame:
                {
                    this.updateLevelState(LevelState.InGame);
                }
                break;
			case LevelState.InGame:
                {
                    if (this.nsBoss.GetComponent<Health>().isDead())
                    {
                        this.updateLevelState(LevelState.Victory);
                    }
                }
                break;
                default: break;
        }
    }
}
