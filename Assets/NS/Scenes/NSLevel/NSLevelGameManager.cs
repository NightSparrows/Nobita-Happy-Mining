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

    [SerializeField] private AudioClip bossBGM;

	// some manager or controller
	private NSLevelStartDirector m_levelStartDirector;          // the director of the starting animation

	private Player m_player;
    private NSBossBehaviorScript m_bossBehavior;
    private AudioSource m_bgmPlayer;

    // variables
    public static NSLevelGameManager s_Instance;
    public LevelState m_state;

    public event Action<LevelState> OnLevelStateChanged;

    // state info
    // wait to game state
    private float m_waitToGameCounter;

	private void Awake()
	{
		s_Instance = this;
        this.m_levelStartDirector = new NSLevelStartDirector();
        this.m_bgmPlayer = this.GetComponent<AudioSource>();
		// test 
		StartLevel();
        // end test
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
				}
                break;
            case LevelState.StartingDirector:
				{
					this.player.transform.position = new Vector3(50, 0, 30);
					this.nsBoss.transform.position = new Vector3(50, 0, 50);

					this.m_bossBehavior = this.nsBoss.GetComponent<NSBossBehaviorScript>();

					this.m_player = this.player.GetComponent<Player>();
                    this.m_player.canMove = false;

                    this.m_bossBehavior.m_player = this.m_player;
					// start the film initialize the camera position
					var laughMotion = new NSBossLaughMotion();
					laughMotion.gameCamera = this.m_camera;
					laughMotion.nsBossGO = this.nsBoss;
                    this.m_levelStartDirector.addMotion(laughMotion);
					this.m_levelStartDirector.initDirector();
                    this.m_bgmPlayer.clip = this.bossBGM;
                    //this.m_bgmPlayer.pitch = 1.0833333333f;
                    this.m_bgmPlayer.Play();
                }
                break;
            case LevelState.WaitToGame:
                {
                    this.m_waitToGameCounter = 2.25f;

                    // a smooth trans to player camera
					this.m_camera.setViewType(GameCamera.ViewType.Smooth);
					this.m_camera.setTarget(this.player.GetComponent<Player>().playerCamera.getTransform());
				}
                break;
            case LevelState.EnterGame:
				{
					this.m_bossBehavior.init();
					this.m_camera.setViewType(GameCamera.ViewType.Immediate);
				}
                break;

			case LevelState.InGame:
                {
                    Debug.Log("Enter in game state");
					// init the game and start battle
					this.m_player.canMove = true;
					// TODO: start the boss ai
				}
                break;
            case LevelState.Victory:
                {
                    this.m_bgmPlayer.Stop();
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
                }
                break;
            case LevelState.WaitToGame:
                {
                    this.m_waitToGameCounter -= Time.deltaTime;
                    if (this.m_waitToGameCounter < 0)
                        this.updateLevelState(LevelState.EnterGame);
                }break;
			case LevelState.EnterGame:
                {
                    this.updateLevelState(LevelState.InGame);
                }
                break;
			case LevelState.InGame:
                {

                }
                break;
                default: break;
        }
    }
}
