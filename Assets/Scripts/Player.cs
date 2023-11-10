using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float moveSpeed = 10f;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 vector = new Vector3(0, 0, 0);
		if (Input.GetKey(KeyCode.W))
		{
			vector.z -= 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			vector.z += 1;
		}
		if (Input.GetKey(KeyCode.A))
		{
			vector.x += 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			vector.x -= 1;
		}
		vector.Normalize();
		vector *= moveSpeed;

		this.transform.Translate(vector * Time.deltaTime);
	}
}
