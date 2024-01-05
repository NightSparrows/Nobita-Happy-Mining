using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NSTileMapManager : MonoBehaviour
{
	[SerializeField] public int gridSizeX = 100;
	[SerializeField] public int gridSizeZ = 100;
	public GameObject[,] m_tiles;

	private void Awake()
	{
		m_tiles = new GameObject[gridSizeX, gridSizeZ];

	}

	public NSTileController createTile(int x, int z)
	{
		if (this.m_tiles[x,z] != null)
		{
			// Tile is created!
			return null;
		}
		Vector3 tilePosition = new Vector3(x * NSTileController.Size, 0, z * NSTileController.Size);
		GameObject tile = Instantiate(new GameObject(), tilePosition, Quaternion.identity);

		NSTileController tileController = tile.AddComponent<NSTileController>();
		tileController.init(this, new Vector2Int(x,z));
		this.m_tiles[x, z] = tile;

		return tileController;
	}

	public void destroyTile(int x, int z)
	{
		GameObject tile = this.m_tiles[x, z];
		if (tile == null)
		{
			Debug.LogWarning("the tile is nothing");
			return;
		}
		NSTileController tileController = tile.GetComponent<NSTileController>();
		if (tileController == null)
		{
			Debug.LogError("the tile has no controller!");
			return;
		}
		tileController.destroyTile();

	}


	private Vector2Int FindNearestDiscretePoint(Vector2 continuousPoint, int n)
	{
		// 將連續位置的浮點數坐標轉換為最接近的整數點
		Vector2Int nearestPoint = new Vector2Int((int)Mathf.Round(continuousPoint.x / (float)n) * n, (int)Mathf.Round(continuousPoint.y / (float)n) * n);
		return nearestPoint;
	}

	// return the tile id( not actual position for path finding)
	public List<Vector2Int> findPath(Vector2 startPos, Vector2 goalPos, int maxSearchNodes = 1000)
	{
		// find the nearest tile position


		Vector2Int start = FindNearestDiscretePoint(startPos, NSTileController.Size);
		Vector2Int goal = FindNearestDiscretePoint(goalPos, NSTileController.Size);

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
				if (closedSet.Contains(neighbor) || m_tiles[neighbor.x, neighbor.y] != null)  // 不是 null 為障礙物
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
		//return (from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y);
		return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
	}

	static Vector2Int[] directions = {
			new Vector2Int(0, 1),   // 上
            new Vector2Int(0, -1),  // 下
            new Vector2Int(1, 0),   // 右
            new Vector2Int(-1, 0)   // 左
        };
	List<Vector2Int> GetNeighbors(Vector2Int position)
	{
		List<Vector2Int> neighbors = new List<Vector2Int>();

		

		foreach (var dir in directions)
		{
			Vector2Int neighbor = position + dir;

			if (neighbor.x >= 0 && neighbor.x < m_tiles.GetLength(0) &&
				neighbor.y >= 0 && neighbor.y < m_tiles.GetLength(1))
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
