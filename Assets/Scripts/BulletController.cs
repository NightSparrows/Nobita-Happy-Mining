using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float m_lifeTime = 1.5f;
    public float m_currentTime = 0f;
	public Vector3 m_velocity;
	protected int m_damage = 25;

	/*
	 * �ΨӴ��bullet�@�Φ��ơA�H�F���z�ĪG�A
	 * �w�]��1�A�N��L�k��z(�@�Τ@��)
	 */
	public int m_triggerCount = 1;

	private void Awake()
	{
		this.m_currentTime = 0;
		this.m_velocity = new Vector3(10, 0, 0);
	}

	public void setVelocity(Vector3 velocity)
	{
		this.m_velocity = velocity;
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

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Enemy")
		{
			/// Handle enemy dealing damage?
			Health enemyHealth = other.GetComponent<Health>();
			enemyHealth.takeDamage(this.m_damage);
			/// ���bullet����
			this.m_triggerCount--;
			if (this.m_triggerCount <= 0)
			{
				Destroy(this.gameObject);
			}
		}

	}
}
