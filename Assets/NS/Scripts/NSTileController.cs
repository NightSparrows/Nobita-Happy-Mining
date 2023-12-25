using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// control the tile attribute
public class NSTileController : MonoBehaviour
{
	// Size of the tile
	public static int Size = 2;

	/// <summary>
	/// Can be delete just use cube for prototype
	/// </summary>
	private NSTileMapManager m_tileManager;
	private Vector2Int m_position;				// position in grid

	public enum TileState
	{
		OnDestroy
	}

	// for other script to subscribe some tile action
	public event System.Action<TileState> OnTileStateChanged;

	// Destroy this tile
	public void destroyTile()
	{
		this.OnTileStateChanged?.Invoke(TileState.OnDestroy);
		this.m_tileManager.m_tiles[this.m_position.x, this.m_position.y] = null;
		Destroy(this.gameObject);
	}

	public void init(NSTileMapManager tileManager, Vector2Int position)
	{
		this.m_tileManager = tileManager;
		this.m_position = position;
	}

	private void Awake()
	{
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
