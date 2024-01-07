
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NSLevelEnemyManager : MonoBehaviour
{
	private NSLevelEnemySpawer m_spawner;
	[SerializeField] private List<NSLevelEnemySpawer.EnemyLevelData> m_spawnData;

	private void Awake()
	{
		this.m_spawner = this.AddComponent<NSLevelEnemySpawer>();
	}

	public void init()
	{
		foreach(var levelData in this.m_spawnData)
		{
			this.m_spawner.addNewSpawnData(levelData);
		}
		this.m_spawner.player = GameManager.Instance.player.GetComponent<Player>();
	}

	public void startSpawn()
	{
		this.m_spawner.startSpawnMob();
	}

	public void stopSpawn()
	{
		this.m_spawner.stopSpawnMob();
	}



	public void Start()
	{
		
	}

	public void Update()
	{
		
	}

}
