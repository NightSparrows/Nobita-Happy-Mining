

using UnityEngine;

public class NSBossVehicleStartupMotion : IAnimationMotion
{
	private GameCamera m_camera;
	private GameObject m_nsBossGO;

	private GameObject m_startupFX;

	private GameObject m_drNastyVehicleGO;
	private Animator m_drNastyVehicleAnimator;

	private bool m_triggerStartupFX = false;

	private Transform m_cameraTransform;
	private Vector3 m_backwardVector;

	public void init()
	{
		this.m_triggerStartupFX = false;

		this.m_cameraTransform = new GameObject().transform;
		this.m_drNastyVehicleGO = this.m_nsBossGO.transform.Find("DrNastyVehicle").gameObject;
		this.m_drNastyVehicleAnimator = this.m_drNastyVehicleGO.GetComponent<Animator>();

		this.m_cameraTransform.position = this.m_drNastyVehicleGO.transform.position;
		this.m_backwardVector = this.m_drNastyVehicleGO.transform.rotation * Vector3.forward;
		this.m_backwardVector.y += 0.1f;
		this.m_backwardVector.Normalize();
		this.m_cameraTransform.position += this.m_backwardVector * 5f;
		this.m_cameraTransform.position += new Vector3(0, 1.5f, 0);
		this.m_cameraTransform.LookAt(this.m_drNastyVehicleGO.transform);

		this.m_camera.setTarget(this.m_cameraTransform);
		this.m_camera.setViewType(GameCamera.ViewType.Smooth);

		this.m_drNastyVehicleAnimator.Play("VehicleStartup");
		this.m_drNastyVehicleAnimator.speed = 1f;
	}

	public bool update(float dt)
	{
		this.m_cameraTransform.position += this.m_backwardVector * 2f * dt;
		float animTime = this.m_drNastyVehicleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (animTime > 0.6f && !this.m_triggerStartupFX)
		{
			GameObject.Instantiate(this.m_startupFX, this.m_drNastyVehicleGO.transform);
			this.m_triggerStartupFX = true;
		}
		if (animTime >= 0.98)
		{
			Debug.Log("done!");
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

	public GameObject startupFX
	{
		set { this.m_startupFX = value; }
	}
}
