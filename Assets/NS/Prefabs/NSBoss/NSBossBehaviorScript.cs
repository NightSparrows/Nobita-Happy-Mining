using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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

	// variables
	private float m_moveSpeed = 10f;

	private CharacterController m_characterController;

	private BossState m_state;

	private AudioSource m_drNastyAudioSource;          // the speaker of boss

	private GameObject m_drNastyGO;
	private GameObject m_vehicleGO;

	private Animator m_drNastyAnimator;
	private Animator m_vehicleAnimator;

	// state variables

	// drill attack variable
	private Vector3 m_drillAttackVector;
	private float m_drillAttackCurrentForwardSpeed;

	// initialize
    public void init()
    {
		this.m_drNastyGO.transform.SetParent(this.m_vehicleMainBone.transform, false);
		this.m_drNastyGO.transform.localPosition = new Vector3(0, 0.005f, 0.004f);
		this.m_drNastyGO.transform.localRotation = Quaternion.identity;
		this.m_drNastyGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_drNastyAnimator.Play("Idle");

		this.m_vehicleAnimator.speed = 1.0f;
		this.m_vehicleAnimator.Rebind();
		this.m_vehicleAnimator.Update(0f);
	}

	private void Awake()
	{
		this.m_characterController = GetComponent<CharacterController>();

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
		this.m_state = state;
		switch (state)
		{
			case BossState.Idle:
				{
				}
				break;
			case BossState.DrillAttack:
				{
					// change to trigger
					this.m_vehicleAnimator.SetTrigger("DrillAttackTrigger");
					//this.m_vehicleAnimator.Play("VehicleDrillAttack");
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
					// TODO: set a timer to decide  moving or attacking, or ultimate
				}
				break;
			case BossState.Moving:
				{
					// TODO: move toward player
					// after finish switch to idle
				}
				break;
			case BossState.DrillAttack:
				{
					// TODO: Doing drill attack
					// after finish switch to idle
					var animState = this.m_vehicleAnimator.GetCurrentAnimatorStateInfo(0);
					float currentTime = animState.normalizedTime % 1;
					if (currentTime >= 0.4 && currentTime <= 0.6)
					{
						this.m_drillAttackCurrentForwardSpeed += this.m_moveSpeed * 0.1f * Time.deltaTime;
					} else if (currentTime > 0.6)
					{
						// TODO: enable attack modifier(Doing damage)
					} else if (currentTime >= 0.99)
					{
						this.changeBossState(BossState.Idle);
					}
					this.m_characterController.Move(this.m_drillAttackCurrentForwardSpeed * this.m_drillAttackVector * Time.deltaTime);
				}
				break;
			default:
				break;
		}

	}
}
