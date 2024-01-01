using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because this level can't be dulipcate so just use static method
public class NSLevelGameManager : MonoBehaviour
{
    public enum LevelState
    {
        Init                    // to initialize all the component of this level
    }

    // objects
    public GameObject ground;
    public GameObject player;
    public NSTileMapManager tileMapManager;
    public GameObject diamondOrePrefab;

    // variables
    public static NSLevelGameManager s_Instance;
    public LevelState m_state;

    public event Action<LevelState> OnLevelStateChanged;

	private void Awake()
	{
		s_Instance = this;
	}

    public static void StartLevel()
	{
		s_Instance.updateLevelState(LevelState.Init);
	}

    protected void updateLevelState(LevelState newState)
    {
        this.m_state = newState;

        switch(newState) { 
            case LevelState.Init:
                {
                    this.player.transform.position = new Vector3(50, 0, 30);
                    this.ground.transform.position = new Vector3(100, 0, 100);

					// for generation just for testing
					for (int i = 0; i < 100; i++)
					{
						int x;
						int z;
                        NSTileController tileController;
						do
						{
							x = UnityEngine.Random.Range(1, 100 - 1);
							z = UnityEngine.Random.Range(1, 100 - 1);
						} while ((tileController = this.tileMapManager.createTile(x,z)) == null);
                        GameObject diamondOreObject = Instantiate(diamondOrePrefab);
                        diamondOreObject.transform.SetParent(tileController.gameObject.transform, false);
					}
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
        
    }
}
