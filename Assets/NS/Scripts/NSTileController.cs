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

	public uint type;

	/// <summary>
	/// Can be delete just use cube for prototype
	/// </summary>
	private GameObject cube;

	public void init()
	{

		/// test for changing this game object
		/// ex add mineral model base on the tile type
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = this.transform;
		cube.AddComponent<BoxCollider>();
		if (cube == null)
		{
			Debug.LogError("Failed to create rock cube mesh");
			return;
		}

		cube.GetComponent<Renderer>().material.color = Color.gray;
		cube.transform.localScale = new Vector3(Size, Size, Size);
		cube.transform.localPosition = new Vector3(0, 0.5f * (float)Size, 0);
		/// end

		// type
		switch (this.type)
		{
			case 0:
				cube.GetComponent<Renderer>().material.color = Color.gray;
				break;
			case 1:
				cube.GetComponent<Renderer>().material.color = Color.red;
				break;
			default:
				break;
		}

		/// 
		NavMeshObstacle navMeshObstacle = this.AddComponent<NavMeshObstacle>();
		navMeshObstacle.shape = NavMeshObstacleShape.Box;
		navMeshObstacle.height = Size;
		navMeshObstacle.radius = Size;

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
