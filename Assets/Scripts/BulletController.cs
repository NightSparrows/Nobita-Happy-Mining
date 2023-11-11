using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float m_lifeTime = 1.5f;
    public float m_currentTime = 0f;
	public Vector3 m_velocity;
	protected int m_damage = 25;

	private void Awake()
	{
		this.m_currentTime = 0;
		this.m_velocity = new Vector3(10, 0, 0);
	}

	// Update is called once per frame
	void Update()
    {
    }

	public int getDamage() { return this.m_damage; }

	private void FixedUpdate()
	{
		if (this.m_currentTime >= this.m_lifeTime)
		{
			Destroy(this.gameObject);
		}
        this.m_currentTime += Time.deltaTime;
		this.transform.Translate(this.m_velocity * Time.deltaTime);
	}
}
