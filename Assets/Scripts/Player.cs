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

	private CharacterController m_characterController;

	public GameObject playerModelGameObject;

	private Health health;
	private Stamina stamina;
	private PlayerExperience exp;
	private Animator animator;

	private bool m_canMove = true;
	public bool m_canShoot = true;
	private float m_moveSpeed = 5f;
	private float rotationSpeed = 10;

	private void Awake()
	{
		//this.m_camera = new PlayerCamera(this);
		this.m_characterController = GetComponent<CharacterController>();
		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
		exp = GetComponent<PlayerExperience>();
		animator = playerModelGameObject.GetComponent<Animator>();
		// test just follow

		this.m_gameCamera.GetComponent<GameCamera>().setTarget(this.transform);
		//this.m_gameCamera.GetComponent<GameCamera>().setTarget(this.m_camera.getTransform());
	}

	// Start is called before the first frame update
	void Start()
	{
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PlayerBullet"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ignore"));
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
			if (vector.magnitude != 0)
			{
				animator.SetBool("isWalking", true);
				// smooth rotation to direction
				Quaternion targetRotation = Quaternion.LookRotation(vector, Vector3.up);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			}
			else
			{
				animator.SetBool("isWalking", false);
			}
			vector *= this.m_moveSpeed;

			this.m_characterController.Move(vector * Time.deltaTime);
			//this.transform.Translate(vector * Time.deltaTime);
		}
		
		if (this.m_canShoot)
		{
			///
			/// remove in future
			///
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GameObject bullet = Instantiate(this.m_currentBulletPrefab, this.transform.position, Quaternion.identity, null);
			}
			///
			/// end remove in future
			///
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
