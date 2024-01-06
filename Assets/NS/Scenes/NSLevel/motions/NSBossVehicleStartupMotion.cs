

using UnityEngine;

public class NSBossVehicleStartupMotion : IAnimationMotion
{
	private GameCamera m_camera;
	private GameObject m_nsBossGO;

	private GameObject m_drNastyVehicleGO;
	private Animator m_drNastyVehicleAnimator;

	public void init()
	{

		this.m_drNastyVehicleGO = this.m_nsBossGO.transform.Find("DrNastyVehicle").gameObject;
		this.m_drNastyVehicleAnimator = this.m_drNastyVehicleGO.GetComponent<Animator>();

		this.m_drNastyVehicleAnimator.Play("VehicleStartup");
		this.m_drNastyVehicleAnimator.speed = 1f;
	}

	public bool update(float dt)
	{
		if (this.m_drNastyVehicleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
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
}
