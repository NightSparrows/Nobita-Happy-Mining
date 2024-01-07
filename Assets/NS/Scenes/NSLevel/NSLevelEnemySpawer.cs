using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NSLevelEnemySpawer : MonoBehaviour
{
	[System.Serializable]
	public class EnemyLevelData
	{
		public GameObject mobPrefab;                 // the mod type of this mod
		public float minSpawnRate;          // the mininum spawn rate to spawn this mob (in second)
		public float maxSpawnRate;          //  the maxinum spawn rate to spawn this mob (in second)
		public float minRadius;             // the mininum radius to spawn this mob around the player
		public float maxRadius;             // the maximum radius to spawn this mob around the player
		public int spawnCount;              // how many mob spawn if the spawning occur
	}

	[System.Serializable]
	public class LevelData
	{
		// runtime normal mob spawn infos
		// maybe can change in runtime to fit the level progress
		public List<EnemyLevelData> enemySpawnInfos;

		public LevelData()
		{
			this.enemySpawnInfos = new List<EnemyLevelData>();
		}

		// TODO boss mob spawning
	}

	public Player player;           // the player handler

	public bool spawnMob = false;
	public LevelData currentLevelData;
	public int spawnRetryCount = 5;                     // the spawn retry count 

	public Vector2 m_spawnSize;

	public class EnemySpawnData
	{
		public GameObject mobPrefab;
		public float currentTimer;                  // current timer for this type mob spawn
		public EnemyLevelData enemyLevelData;       // a reference to the spawning data
	}
	private List<EnemySpawnData> enemySpawnDataList;            // the runtime enemy spawning counter

	// constructor
	private void Awake()
	{
		this.enemySpawnDataList = new List<EnemySpawnData>();
		this.m_spawnSize = new Vector2(98, 98);
	}

	public void init(in string levelDataJsonText)
	{
		this.currentLevelData = JsonUtility.FromJson<LevelData>(levelDataJsonText);
		foreach (EnemyLevelData enemyLevelData in this.currentLevelData.enemySpawnInfos)
		{
			EnemySpawnData enemySpawnData = new EnemySpawnData();
			enemySpawnData.enemyLevelData = enemyLevelData;
			enemySpawnData.currentTimer = 0;        // initialize
													// TODO find the prefab for this mob type and add to 
			enemySpawnData.mobPrefab = enemyLevelData.mobPrefab;

			this.enemySpawnDataList.Add(enemySpawnData);

		}
	}

	public void startSpawnMob()
	{
		this.spawnMob = true;
		Debug.Log("Turn on mob spawning");
	}

	public void stopSpawnMob()
	{
		this.spawnMob = false;
		Debug.Log("Turn off mob spawning");
	}

	public void addNewSpawnData(EnemyLevelData levelData)
	{
		EnemySpawnData enemySpawnData = new EnemySpawnData();
		enemySpawnData.enemyLevelData = levelData;
		enemySpawnData.currentTimer = 0;        // initialize
												// TODO find the prefab for this mob type and add to 
		enemySpawnData.mobPrefab = levelData.mobPrefab;

		this.enemySpawnDataList.Add(enemySpawnData);
	}

	// Start is called before the first frame update
	void Start()
	{
		// for testing
		//LevelData testLevelData = new LevelData();
		//testLevelData.enemySpawnInfos.Add(new EnemyLevelData
		//{
		//	mobType = 0,
		//	minSpawnRate = 5f,
		//	maxSpawnRate = 10f,
		//	minRadius = 10f,
		//	maxRadius = 50f,
		//	spawnCount = 2
		//});

		//string levelDataJsonText = JsonUtility.ToJson(testLevelData);
		//this.init(levelDataJsonText);
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.spawnMob)
			return;

		// Spawn mobs
		foreach (EnemySpawnData enemySpawnData in this.enemySpawnDataList)
		{
			enemySpawnData.currentTimer += Time.deltaTime;
			if (enemySpawnData.enemyLevelData.minSpawnRate > enemySpawnData.currentTimer)
			{
				continue;
			}
			else if (enemySpawnData.currentTimer >= enemySpawnData.enemyLevelData.minSpawnRate && enemySpawnData.currentTimer <= enemySpawnData.enemyLevelData.maxSpawnRate)
			{
				// Random if not spawn just continue
				if (Random.value < 0.3f)
					continue;
			}
			// spawn
			Debug.Log("Start spawn mob");
			for (int i = 0; i < enemySpawnData.enemyLevelData.spawnCount; i++)
			{
				float x;
				float z;
				int retryCount = 0;
				do
				{
					bool correctRadius = false;
					do
					{
						x = Random.Range(5, this.m_spawnSize.x);
						z = Random.Range(5, this.m_spawnSize.y);

						float length = (new Vector3(x * NSTileController.Size, 0, z * NSTileController.Size) - this.player.transform.position).magnitude;


						if (length >= enemySpawnData.enemyLevelData.minRadius && length <= enemySpawnData.enemyLevelData.maxRadius)
						{
							correctRadius = true;
						}

					} while (!correctRadius);

					retryCount++;

				} while (retryCount < this.spawnRetryCount);
				if (retryCount > this.spawnRetryCount)
				{
					Debug.LogWarning("Failed to spawn a mob need to be spawn");
					break;
				}

				GameObject enemyGameObject = Instantiate(enemySpawnData.mobPrefab, new Vector3(x * NSTileController.Size, 0, z * NSTileController.Size), Quaternion.identity/* or random rotation? */);
				enemyGameObject.transform.localScale = Vector3.one;
				Debug.Log("Enemy spawned");
			}

			// reset the timer
			enemySpawnData.currentTimer = 0;
		}

	}
}
