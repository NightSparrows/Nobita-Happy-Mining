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
	private Health m_health;

	public void init()
	{
		this.tag = "Mineral";
		BoxCollider collider = this.AddComponent<BoxCollider>();
		collider.center = new Vector3(0, 1, 0);
		collider.size = new Vector3(2, 2, 2);
		collider.isTrigger = true;

		this.m_health = this.AddComponent<Health>();

		/// test for changing this game object
		/// ex add mineral model base on the tile type
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = this.transform;
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
				this.m_health.init(100);
				break;
			case 1:
				cube.GetComponent<Renderer>().material.color = Color.red;
				this.m_health.init(200);
				break;
			default:
				break;
		}
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
