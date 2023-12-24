using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	enum PlayerState
	{
		Idle,
		Walk,
		Sleep
	}

	private PlayerState m_state;
	private PlayerCamera m_camera;
	public MiningPickaxe m_miningPickaxe;
	public Camera m_gameCamera;

	// just public now dont know
	//public GameObject m_currentBulletPrefab;

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

	// digging variable
	private GameObject m_currentMineObject;		// current mining object
	private bool m_isMining = false;
	private float m_currentMiningTime = 0;
	private int m_miningDamage = 10;			// each mine take how many damage
	private float m_miningSpeed = 2f;           // the speed of mine down
	private float m_miningRange = 3f;
	// Range Detector
	[SerializeField] RangeDetector rangeDetector;

	private void Awake()
	{
		this.m_state = PlayerState.Idle;

		this.m_characterController = GetComponent<CharacterController>();
		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
		exp = GetComponent<PlayerExperience>();
		animator = playerModelGameObject.GetComponent<Animator>();

		// test just follow
		this.m_camera = new PlayerCamera(this);
		this.m_gameCamera.GetComponent<GameCamera>().setTarget(this.m_camera.getTransform());

	}

	// Start is called before the first frame update
	void Start()
	{
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PlayerBullet"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ignore"));

		// Range Detector
		rangeDetector.OnRangeEnter += CollectExp;
	}

    // Update is called once per frame
    void Update()
	{
		switch (this.m_state)
		{
			case PlayerState.Idle:
				{
					animator.SetBool("isWalking", false);
					animator.SetBool("isSleeping", false);
					if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
						this.m_state = PlayerState.Walk;
					else if (Input.GetKey(KeyCode.B))
						this.m_state = PlayerState.Sleep;
				}
				break;
			case PlayerState.Walk:
				{
					animator.SetBool("isSleeping", false);
					animator.SetBool("isWalking", true);
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
					if (Input.GetKey(KeyCode.B))
					{
						this.m_state = PlayerState.Sleep;
					}
					vector.Normalize();
					if (vector.magnitude != 0)
					{
						// smooth rotation to direction
						Quaternion targetRotation = Quaternion.LookRotation(vector, Vector3.up);
						transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
						vector *= this.m_moveSpeed;

						this.m_characterController.SimpleMove(vector);
					}
					else
					{
						this.m_state = PlayerState.Idle;
					}
				}
				break;
			case PlayerState.Sleep:
				{
					animator.SetBool("isWalking", false);
					animator.SetBool("isSleeping", true);
					Debug.Log("sleeping");
					this.m_miningPickaxe.stopMining();
					/// TODO: 回復Stamina
					if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
						this.m_state = PlayerState.Walk;
				}
				break;
			default:
				Debug.LogWarning("Unknown player state");
				break;

		}

		RaycastHit hit;
		int miniralLayer = 1 << LayerMask.NameToLayer("Mineral");
		//Debug.Log("miniral layer " + miniralLayer);
		if (Physics.Raycast(transform.position, transform.forward * Time.deltaTime, out hit, this.m_miningRange, miniralLayer))
		{
			Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
			if (hit.collider.gameObject.CompareTag("Mineral") && this.m_state != PlayerState.Sleep)
			{
				this.m_currentMineObject = hit.collider.gameObject;
				this.m_isMining = true;
				this.m_miningPickaxe.startMining();
			}
		} else
		{
			//Debug.Log("not mining");
			this.m_currentMineObject = null;
			this.m_isMining = false;
			this.m_miningPickaxe.stopMining();
		}
		// �W�ߩ�player state
		if (this.m_isMining)
		{
			this.m_currentMiningTime += Time.deltaTime;
			if (this.m_currentMiningTime >= this.m_miningSpeed && this.m_currentMineObject != null)
			{
				this.m_currentMineObject.GetComponent<Health>().takeDamage(this.m_miningDamage);
				Debug.Log("Mine! current health: " + this.m_currentMineObject.GetComponent<Health>().getCurrentHealth());
				this.m_currentMiningTime = 0;
			}

		}

		float scrollInput = -Input.mouseScrollDelta.y;
		float targetDistance = this.m_camera.getDistance() + Time.deltaTime * scrollInput * 50f/* scroll speed */;
        if ( targetDistance >= 50f)
        {
			targetDistance = 50f;
        } else if (targetDistance <= 10f)
		{
			targetDistance = 10f;
		}
		this.m_camera.setDistance(targetDistance);
        this.m_camera.update(Time.deltaTime);

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

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.tag == "Exp")
	//	{
	//		exp.CurrentPlayerExp += 1;
	//		Destroy(other.gameObject);
	//	}
	//}

	// Exp event
	void CollectExp(Collider other)
    {
		if (other.tag == "Exp")
		{
			Debug.Log("exp in range");
			other.gameObject.GetComponent<TargetMovement>().enabled = true;
			//
			//other.gameObject.GetComponent<BaseMovement>().enableMove = true;
		}
	}
}
