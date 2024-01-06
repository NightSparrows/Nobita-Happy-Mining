


using UnityEngine;

public class NSBossJumpMotion : IAnimationMotion
{
	private GameCamera m_camera;
	private GameObject m_nsBossGO;

	private GameObject m_drNastyGO;         // the game object of dr nasty model
	private Animator m_drNastyAnimator;
	private GameObject m_drNastyVehicleGO;
	private Animator m_drNastyVehicleAnimator;

	private Vector3 m_horizonVelocity = new Vector3(0, 0, -2.77f);
	private Vector3 m_verticalVelocity = new Vector3(0, 5, 0);
	private float m_gravityCoeff = 4.2f;
	private float m_counter;
	private float m_finishTime = 2f;

	public void init()
	{

		this.m_drNastyGO = this.m_nsBossGO.transform.Find("DrNastyModel").gameObject;
		this.m_drNastyAnimator = this.m_drNastyGO.GetComponent<Animator>();

		this.m_drNastyVehicleGO = this.m_nsBossGO.transform.Find("DrNastyVehicle").gameObject;
		this.m_drNastyVehicleAnimator = this.m_drNastyVehicleGO.GetComponent<Animator>();

		this.m_counter = 0;
	}

	public bool update(float dt)
	{
		this.m_camera.targetTransform.LookAt(this.m_drNastyGO.transform);
		this.m_drNastyGO.transform.position += this.m_horizonVelocity * dt;
		this.m_drNastyGO.transform.position += this.m_verticalVelocity * dt;
		this.m_verticalVelocity.y -= this.m_gravityCoeff * dt;
		this.m_counter += dt;

		if (this.m_counter >= this.m_finishTime)
		{
			this.m_drNastyGO.transform.SetParent(this.m_nsBossGO.GetComponent<NSBossBehaviorScript>().m_vehicleMainBone.transform, false);
			this.m_drNastyGO.transform.localPosition = new Vector3(0, 0.005f, 0.004f);
			this.m_drNastyGO.transform.localRotation = Quaternion.identity;
			this.m_drNastyGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			return true;
		}

		return false;
	}

	public GameCamera gameCamera
	{
		get { return m_camera; }
		set
		{
			this.m_camera = value;
		}
	}

	public GameObject nsBossGO
	{
		get { return m_nsBossGO; }
		set { m_nsBossGO = value; }
	}
}
