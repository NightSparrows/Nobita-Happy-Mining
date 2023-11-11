using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float m_lifeTime = 1.5f;
    public float m_currentTime = 0f;

	private void Awake()
	{
		this.m_currentTime = 0;
	}

	// Update is called once per frame
	void Update()
    {
    }

	private void FixedUpdate()
	{
		if (this.m_currentTime >= this.m_lifeTime)
		{
			Destroy(this.gameObject);
		}
        this.m_currentTime += Time.deltaTime;
	}
}
