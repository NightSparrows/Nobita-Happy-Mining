using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera //: MonoBehaviour
{
	public float m_followSpeed = 2.0f;
	private Player m_player;
	private GameObject m_object;        // my virtual object

	private SmoothFloat m_distance;
	private SmoothFloat m_pitch;
	private SmoothFloat m_yaw;

	public PlayerCamera(Player player)
	{
		this.m_player = player;
		this.m_object = new GameObject("PlayerCamera");

		this.m_distance = new SmoothFloat(50f);
		this.m_pitch = new SmoothFloat(45f);
		this.m_yaw = new SmoothFloat(-45f);
	}

	public Transform getTransform()
	{
		return this.m_object.transform;
	}

    // Update is called once per frame
    public void update()
	{
		float horizontalDistance = this.m_distance * Mathf.Cos(Mathf.Deg2Rad * this.m_pitch);
		float verticalDistance = this.m_distance * Mathf.Sin(Mathf.Deg2Rad * this.m_pitch);

		Vector3 newPos = this.m_player.transform.position;
		newPos.y += verticalDistance;
		newPos.x += horizontalDistance * Mathf.Sin(Mathf.Deg2Rad * this.m_yaw);
		newPos.z += horizontalDistance * Mathf.Cos(Mathf.Deg2Rad * this.m_yaw);

		this.m_object.transform.position = newPos;
		this.m_object.transform.LookAt(this.m_player.transform.position);
	}
}
