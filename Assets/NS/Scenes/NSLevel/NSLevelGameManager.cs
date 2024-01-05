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
        InGame,                  // in game
        Victory
    }

	// some manager or controller
	private NSLevelStartDirector m_levelStartDirector;          // the director of the starting animation

    // objects
    public GameCamera m_camera;                                 // the main camera in scene
    public GameObject player;
    public GameObject nsBoss;
    public NSTileMapManager tileMapManager;
    public GameObject diamondOrePrefab;

    private Player m_player;
    private NSBossBehaviorScript m_bossBehavior;

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
        StartLevel();
	}

    public static void StartLevel()
	{
		//s_Instance.updateLevelState(LevelState.Init);
		s_Instance.updateLevelState(LevelState.StartingDirector);
	}

    protected void updateLevelState(LevelState newState)
    {
        this.m_state = newState;

        switch(newState) { 
            case LevelState.Init:
                {
                    this.player.transform.position = new Vector3(50, 0, 30);

					// for generation just for testing
					//for (int i = 0; i < 100; i++)
					//{
					//	int x;
					//	int z;
     //                   NSTileController tileController;
					//	do
					//	{
					//		x = UnityEngine.Random.Range(1, 100 - 1);
					//		z = UnityEngine.Random.Range(1, 100 - 1);
					//	} while ((tileController = this.tileMapManager.createTile(x,z)) == null);
     //                   GameObject diamondOreObject = Instantiate(diamondOrePrefab);
     //                   diamondOreObject.transform.SetParent(tileController.gameObject.transform, false);
					//}
				}
                break;
            case LevelState.StartingDirector:
                {
                    this.m_bossBehavior = this.nsBoss.GetComponent<NSBossBehaviorScript>();

					this.m_player = this.player.GetComponent<Player>();
                    this.m_player.canMove = false;
                    // start the film initialize the camera position
                    this.m_levelStartDirector.gameCamera = this.m_camera;
                    this.m_levelStartDirector.nsBossGO = this.nsBoss;
                    this.m_levelStartDirector.initDirector();
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
            case LevelState.InGame:
                {
                    Debug.Log("Enter in game state");
					// init the game and start battle
					this.m_camera.setViewType(GameCamera.ViewType.Immediate);
					this.m_player.canMove = true;
                    this.m_bossBehavior.init();
					// TODO: start the boss ai
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
                        this.updateLevelState(LevelState.InGame);
                }break;
            case LevelState.InGame:
                {

                }
                break;
                default: break;
        }
    }
}
