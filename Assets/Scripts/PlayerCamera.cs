using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera
{
	public float m_followSpeed = 2.0f;
	public Player m_player;
	private GameObject m_object;		// my virtual object

	public PlayerCamera(Player player)
	{
		this.m_player = player;
		this.m_object = new GameObject("PlayerCamera");
		this.m_object.transform.LookAt(new Vector3(10, -20, -20));
	}

	public Transform getTransform()
	{
		return this.m_object.transform;
	}

    // Update is called once per frame
    public void update()
	{
		Vector3 newPos = new Vector3(this.m_player.transform.position.x, this.m_player.transform.position.y, this.m_player.transform.position.z);
		newPos.x -= 10f;
		newPos.z += 20f;
		newPos.y += 20f;
		this.m_object.transform.position = newPos;
	}
}
