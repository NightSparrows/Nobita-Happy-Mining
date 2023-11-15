﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    public GameObject tilePrefab;
	[SerializeField] public int gridSizeX = 100;
	[SerializeField] public int gridSizeZ = 100;
	public GameObject[,] tiles;

	private void Awake()
	{
		tiles = new GameObject[gridSizeX, gridSizeZ];

		// test
        for (int i = 0; i < 100; i++)
		{
			int x;
			int z;
			do
			{
				x = Random.Range(1, this.gridSizeX - 1);
				z = Random.Range(1, this.gridSizeZ - 1);
			} while (tiles[x, z] != null);
			Vector3 tilePosition = new Vector3(x * TileController.Size, 0, z * TileController.Size);
            GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
			tiles[x, z] = tile;
        }
	}

	public class GridLocation
	{
		public bool m_filled, m_impassable, m_unPathable, m_hasBeenUsed, m_isViewable;
		public float m_fscore, m_cost, m_currentDist;
		public Vector2Int m_parent, m_pos;

		public GridLocation(float cost, bool filled)
		{
			this.m_cost = cost;
			this.m_filled = filled;

			this.m_hasBeenUsed = false;
			this.m_isViewable = false;
			this.m_unPathable = false;
			this.m_impassable = false;
		}
		public GridLocation(Vector2Int pos, float cost, bool filled, float fscore)
		{
			this.m_pos = pos;
			this.m_fscore = fscore;
			this.m_cost = cost;
			this.m_filled = filled;

			this.m_hasBeenUsed = false;
			this.m_isViewable = false;
			this.m_unPathable = false;
		}

		public void setNode(Vector2Int parent, float fscore, float currentDist)
		{
			this.m_parent = parent;
			this.m_fscore = fscore;
			this.m_currentDist = currentDist;
		}

		public virtual void setToFilled(bool impassable)
		{
			this.m_filled = true;
			this.m_impassable = impassable;
		}

	}

	public List<Vector2Int> findPathV2(Vector2Int start, Vector2Int end, int maxSearchNodes = 1000)
	{

		return null;
	}

	// return the tile id( not actual position for path finding)
	public List<Vector2Int> findPath(Vector2Int start, Vector2Int goal, int maxSearchNodes = 1000)
	{
		SortedDictionary<float, Vector2Int> openSet = new SortedDictionary<float, Vector2Int>();
		HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
		Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
		Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
		Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

		openSet.Add(0, start);
		gScore[start] = 0;
		fScore[start] = HeuristicCostEstimate(start, goal);

		int nodesSearched = 0;

		while (openSet.Count > 0 && nodesSearched < maxSearchNodes)
		{
			var current = openSet.First().Value;
			openSet.Remove(openSet.First().Key);
			nodesSearched++;

			if (current == goal)
			{
				// 找到路径，现在你可以从 goal 往回走，重建路径
				return ReconstructPath(cameFrom, start, goal);
			}

			/// test a node
			closedSet.Add(current);

			List<Vector2Int> neighbors = GetNeighbors(current);

			foreach (Vector2Int neighbor in neighbors)
			{
				if (closedSet.Contains(neighbor) || tiles[neighbor.x, neighbor.y] != null)	// 不是 null 為障礙物
					continue;

				float tentativeGScore = gScore[current] + 1; // 在这里，代价为1，你可以根据实际情况调整

				if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
				{
					cameFrom[neighbor] = current;
					gScore[neighbor] = tentativeGScore;
					fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, goal);

					if (!openSet.ContainsKey(fScore[neighbor]))
					{
						openSet.Add(fScore[neighbor], neighbor);
					}
				}
			}
		}

		//Debug.Log("No path found.");
		return null;
	}

	float HeuristicCostEstimate(Vector2Int from, Vector2Int to)
	{
		// 使用曼哈顿距离（Manhattan Distance）作为启发函数
		return (from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y);
	}

	List<Vector2Int> GetNeighbors(Vector2Int position)
	{
		List<Vector2Int> neighbors = new List<Vector2Int>();

		Vector2Int[] directions = {
			new Vector2Int(0, 1),   // 上
            new Vector2Int(0, -1),  // 下
            new Vector2Int(1, 0),   // 右
            new Vector2Int(-1, 0)   // 左
        };

		foreach (var dir in directions)
		{
			Vector2Int neighbor = position + dir;

			if (neighbor.x >= 0 && neighbor.x < tiles.GetLength(0) &&
				neighbor.y >= 0 && neighbor.y < tiles.GetLength(1))
			{
				neighbors.Add(neighbor);
			}
		}

		return neighbors;
	}

	List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int goal)
	{
		List<Vector2Int> path = new List<Vector2Int>();
		Vector2Int current = goal;

		while (current != start)
		{
			path.Add(current);
			current = cameFrom[current];
		}

		path.Add(start);
		path.Reverse();

		//Debug.Log("Path found: " + string.Join(" -> ", path));
		return path;
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
