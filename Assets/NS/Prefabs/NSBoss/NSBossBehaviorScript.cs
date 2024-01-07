using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NSBossBehaviorScript : MonoBehaviour 
{

	public enum BossState
	{
		None,
		Idle,
		Moving,
		DrillAttack,
		BarrelAttack,
		AirStrike,
		Death
	}

    [SerializeField] public Player m_player;
    [SerializeField] public MultiAimConstraint m_multiAimConstraint;
	[SerializeField] public GameObject m_vehicleMainBone;
	[SerializeField] public NSBossDrillController m_drillController;

	// variables
	[SerializeField] private float m_moveSpeed = 8f;
	[SerializeReference] private float m_rotateSpeed = 90f;
	[SerializeField] private BossState m_state;

	// objects
	private Health m_health;

	private CharacterController m_characterController;
	private AudioSource m_drNastyAudioSource;          // the speaker of boss

	private GameObject m_drNastyGO;
	public GameObject m_vehicleGO;

	private Animator m_drNastyAnimator;
	private Animator m_vehicleAnimator;

	// state variables

	// idle variable
	private float m_idleCounter;

	// move variable
	private float m_moveCounter;

	// drill attack variable
	public Vector3 m_drillAttackVector;
	public float m_drillAttackCurrentForwardSpeed;

	// initialize
    public void init()
    {
		this.m_health = this.GetComponent<Health>();
		this.m_health.init(300000);

		this.m_drNastyGO.transform.SetParent(this.m_vehicleMainBone.transform, false);
		this.m_drNastyGO.transform.localPosition = new Vector3(0, 0.005f, 0.004f);
		this.m_drNastyGO.transform.localRotation = Quaternion.identity;
		this.m_drNastyGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_drNastyAnimator.Rebind();
		this.m_drNastyAnimator.Update(0f);
		//this.m_drNastyAnimator.Play("Idle");

		this.m_vehicleAnimator.speed = 1.0f;
		this.m_vehicleAnimator.Rebind();
		this.m_vehicleAnimator.Update(0f);

		// prepare to enable this behavior
		this.changeBossState(BossState.Idle);
	}

	private void Awake()
	{
		this.m_characterController = this.GetComponent<CharacterController>();

		this.m_state = BossState.None;
		this.m_drNastyGO = this.transform.Find("DrNastyModel").gameObject;
		this.m_vehicleGO = this.transform.Find("DrNastyVehicle").gameObject;

		// get the components for necessary
		this.m_drNastyAnimator = this.m_drNastyGO.GetComponent<Animator>();
		this.m_vehicleAnimator = this.m_vehicleGO.GetComponent<Animator>();

		this.m_drNastyAudioSource = this.m_drNastyGO.GetComponent<AudioSource>();

		if (this.m_drNastyAnimator == null || this.m_vehicleAnimator == null )
		{
			Debug.LogError("Failed to get the animators");
		}

	}

	// Start is called before the first frame update
	void Start()
	{
		//this.m_vehicleAnimator.SetTrigger("StartUpTrigger");
	}

	public void changeBossState(BossState state)
	{
		/// TODO 
		/// 
		// the state end api
		/// 
		switch (this.m_state)
		{
			case BossState.DrillAttack:
				this.m_drillController.enableDamage = false;
				break;
			default:
				break;

		}

		this.m_state = state;
		switch (state)
		{
			case BossState.Idle:
				{
					// TODO: set a timer to decide  moving or attacking, or ultimate
					this.m_idleCounter = Random.Range(0, 5);
				}
				break;
			case BossState.Moving:
				{
					this.m_moveCounter = Random.Range(3, 5);
				}
				break;
			case BossState.DrillAttack:
				{
					// if doing drill attack, need to initialize the vector
					// this is just for testing 
					this.m_drillAttackVector = this.m_vehicleGO.transform.rotation * Vector3.forward;
					this.m_drillAttackVector.Normalize();

					// initialize the forward speed
					this.m_drillAttackCurrentForwardSpeed = 0;
					this.m_vehicleAnimator.SetTrigger("drillAttackTrigger");
					this.m_drillController.enableDamage = true;
				}
				break;
			case BossState.BarrelAttack:
				{

					this.m_vehicleAnimator.SetTrigger("BarrelAttackTrigger");
				}
				break;
			default: break;
		}
	}

	// Update is called once per frame
	void Update()
    {
		switch (this.m_state)
		{
			case BossState.None: break;		// do nothing
			case BossState.Idle:
				{
					this.m_idleCounter -= Time.deltaTime;

                    // rotate to face the player
                    Vector3 vector = this.m_player.transform.position - this.m_vehicleGO.transform.position;
					vector.Normalize();
					Quaternion targetRotation = Quaternion.LookRotation(vector, Vector3.up);
					this.m_vehicleGO.transform.rotation = Quaternion.RotateTowards(this.m_vehicleGO.transform.rotation, targetRotation, this.m_rotateSpeed * Time.deltaTime);
					//this.m_vehicleGO.transform.rotation = targetRotation;

					if (this.m_idleCounter <= 0)
					{
						int behaviour = Random.Range(0, 3);
						switch (behaviour)
						{
							case 0:
								{
									this.changeBossState(BossState.BarrelAttack);
								}
								break;
							case 1:
								{
									this.changeBossState(BossState.BarrelAttack);
								}
								break;
							default:
								{
									if (Vector3.Distance(this.m_player.transform.position, this.m_vehicleGO.transform.position) <= 20)
									{
										this.changeBossState(BossState.DrillAttack);
									}
									else
									{
										this.changeBossState(BossState.Moving);
									}
								}
								break;
						}
						// TODO if ultimate is prepared use it

						// just for testing
						// TODO change to wanted state
					}

				}
				break;
			case BossState.Moving:
				{
					// TODO: rotate or move toward player
					this.m_moveCounter -= Time.deltaTime;
					// rotate to face the player
					Vector3 vector = this.m_player.transform.position - this.m_vehicleGO.transform.position;
					Vector3 unitVector = Vector3.Normalize(vector);
					Quaternion targetRotation = Quaternion.LookRotation(unitVector, Vector3.up);
					this.m_vehicleGO.transform.rotation = Quaternion.RotateTowards(this.m_vehicleGO.transform.rotation, targetRotation, this.m_rotateSpeed * Time.deltaTime);

					Vector3 moveVector = this.m_vehicleGO.transform.rotation * Vector3.forward;
					moveVector.Normalize();

					if (vector.magnitude > 5)
					{
						this.move(moveVector * this.m_moveSpeed * Time.deltaTime);
					}

					// after finish switch to idle
					if (this.m_moveCounter <= 0)
					{
						this.changeBossState(BossState.Idle);
					}
				}
				break;
			case BossState.DrillAttack:
				{
					// TODO: Doing drill attack
					// Do the stuff in animation handler (which I don't like this)
				}
				break;
			case BossState.BarrelAttack:
				{
					// TODO: Doing barrel attack
					// Do the stuff in animation handler too (which I don't like this too)
				}
				break;
			default:
				break;
		}

	}

	public void move(Vector3 vector)
	{
		this.m_characterController.Move(vector);
		//this.transform.position += vector;
	}

	public float moveSpeed
	{
		get { return this.m_moveSpeed; }
	}

	public float rotateSpeed
	{
		get { return this.m_rotateSpeed; }
	}

}
