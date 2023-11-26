using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NSEnemyManager : MonoBehaviour
{
	[System.Serializable]
	public class EnemyLevelData
	{
		public int mobType;                 // the mod type of this mod
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

	public NSTileMapManager tileMapManager;
	public Player player;           // the player handler
	public List<GameObject> currentEnemyList;           // all the alive enemy list

	public bool spawnMob = false;
	public LevelData currentLevelData;
	public int spawnRetryCount = 5;                     // the spawn retry count 

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
			switch (enemyLevelData.mobType)
			{
				case 0:
					// for test enemy
					enemySpawnData.mobPrefab = Resources.Load<GameObject>("NS/Prefabs/Enemy/TestEnemyPrefab");
					Debug.Assert(enemySpawnData.mobPrefab != null, "the test enemy prefab not found!");
					break;
				default:
					Debug.LogError("Unknown mob type");
					continue;
			}

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

	// Start is called before the first frame update
	void Start()
	{
		// for testing
		LevelData testLevelData = new LevelData();
		testLevelData.enemySpawnInfos.Add(new EnemyLevelData
		{
			mobType = 0,
			minSpawnRate = 5f,
			maxSpawnRate = 10f,
			minRadius = 10f,
			maxRadius = 50f,
			spawnCount = 2
		});

		string levelDataJsonText = JsonUtility.ToJson(testLevelData);
		this.init(levelDataJsonText);
		this.startSpawnMob();

	}

	// Update is called once per frame
	void Update()
	{
		// dealing the current mobs
		for (int i = currentEnemyList.Count - 1; i >= 0; i--)
		{
			GameObject enemy = this.currentEnemyList[i];
			Health health = enemy.GetComponent<Health>();
			NSIEnemyController enemyController = enemy.GetComponent<NSIEnemyController>();
			if (enemyController == null)
			{
				Debug.LogError("The enemy should have the enemy controller interface!");
				continue;
			}
			if (health.isDead() && enemyController.isDeathAnimationOver())
			{
				this.currentEnemyList.RemoveAt(i);
				//GameObject e = Instantiate(expPrefab, enemy.transform.position, Quaternion.identity);
				//e.GetComponent<Exp>().SetPlayer(player);
				//e.transform.Translate(0f, -2.5f, 0f);
				Destroy(enemy);
			}
		}

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
				int x;
				int z;
				int retryCount = 0;
				do
				{
					bool correctRadius = false;
					do
					{
						x = Random.Range(0, this.tileMapManager.gridSizeX);
						z = Random.Range(0, this.tileMapManager.gridSizeZ);

						float length = (new Vector3(x * NSTileController.Size, 0, z * NSTileController.Size) - this.player.transform.position).magnitude;


						if (length >= enemySpawnData.enemyLevelData.minRadius && length <= enemySpawnData.enemyLevelData.maxRadius)
						{
							correctRadius = true;
						}

					} while (!correctRadius);



					if (this.tileMapManager.tiles[x, z] == null)            // is a empty tile
						break;

					retryCount++;

				} while (retryCount < this.spawnRetryCount);
				if (retryCount > this.spawnRetryCount)
				{
					Debug.LogWarning("Failed to spawn a mob need to be spawn");
					break;
				}

				GameObject enemyGameObject = Instantiate(enemySpawnData.mobPrefab, new Vector3(x * NSTileController.Size, 0, z * NSTileController.Size), Quaternion.identity/* or random rotation? */);
				NSIEnemyController enemyController = enemyGameObject.GetComponent<NSIEnemyController>();
				Debug.Assert(enemyController != null, "Enemy should have controller interface!");
				enemyController.initController(this.tileMapManager, this.player);
				this.currentEnemyList.Add(enemyGameObject);
				Debug.Log("Enemy spawned");
			}

			// reset the timer
			enemySpawnData.currentTimer = 0;
		}

	}
}
