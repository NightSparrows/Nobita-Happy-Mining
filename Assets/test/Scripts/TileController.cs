using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// control the tile attribute
public class TileController : MonoBehaviour
{
    // Size of the tile
    public static int Size = 2;

    public uint type;
    private GameObject cube;


	private void Awake()
	{
        cube = transform.Find("RockCube").gameObject;
        if (cube == null)
        {
            Debug.LogError("Failed to find rock cube mesh");
            return;
        }

        cube.GetComponent<Renderer>().material.color = Color.gray;
        cube.transform.localScale = new Vector3(Size, Size, Size);
        cube.transform.localPosition = new Vector3(0, 0.5f * (float)Size, 0);


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
