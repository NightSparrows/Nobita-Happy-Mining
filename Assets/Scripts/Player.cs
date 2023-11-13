using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerCamera m_camera;
	public Camera m_gameCamera;

	// just public now dont know
	public GameObject m_currentBulletPrefab;
	
	private Health health;
	private Stamina stamina;
	private PlayerExperience exp;

	private bool m_canMove = true;
	public bool m_canShoot = true;
	private float m_moveSpeed = 10f;

	private void Awake()
	{
		//this.m_camera = new PlayerCamera(this);
		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
		exp = GetComponent<PlayerExperience>();
		// test just follow

		this.m_gameCamera.GetComponent<GameCamera>().setTarget(this.transform);
		//this.m_gameCamera.GetComponent<GameCamera>().setTarget(this.m_camera.getTransform());
	}

	// Start is called before the first frame update
	void Start()
	{
    }

    // Update is called once per frame
    void Update()
    {
		if (this.m_canMove)
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
			vector *= this.m_moveSpeed;

			this.transform.Translate(vector * Time.deltaTime);
		}
		
		if (this.m_canShoot)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				GameObject bullet = Instantiate(this.m_currentBulletPrefab, this.transform.position, Quaternion.identity, null);
			}
		}

		//this.m_camera.update();

		/*
		 * ---- Test the Update of Health, Stamina, Exp ----
		 * 
		 *		Remove anytime you want
		 *		
		 * -------------------------------------------------
		 */
		if (Input.GetKey(KeyCode.Z))
        {
			stamina.CurrentStamina -= 1;
        }
		if (Input.GetKey(KeyCode.X))
        {
			health.takeDamage(1);
        }
		if (Input.GetKey(KeyCode.C))
        {
			exp.CurrentPlayerExp += 1;
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exp")
		{
			exp.CurrentPlayerExp += 1;
			Destroy(other.gameObject);
		}
	}

}
