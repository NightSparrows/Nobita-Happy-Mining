
using System;
using UnityEngine;
public class NSBossLaughMotion : IAnimationMotion
{
	private GameCamera m_camera;
	private GameObject m_nsBossGO;

	private Transform m_motionCamera;		// thie motion's camera
	private GameObject m_drNastyGO;         // the game object of dr nasty model
	private Animator m_drNastyAnimator;


	private bool m_firstFramePass;
	public void init()
	{
		this.m_motionCamera = new GameObject().transform;
		Debug.Log(this.m_camera);
		this.m_camera.setTarget(this.m_motionCamera);
		this.m_camera.setViewType(GameCamera.ViewType.Immediate);

		this.m_drNastyGO = this.m_nsBossGO.transform.Find("DrNastyModel").gameObject;
		this.m_drNastyAnimator = this.m_drNastyGO.GetComponent<Animator>();

		// init 
		this.m_motionCamera.position = this.m_drNastyGO.transform.position;
		this.m_motionCamera.position += new Vector3(5, 5, 5);
		this.m_motionCamera.LookAt(this.m_drNastyGO.transform);

		this.m_drNastyAnimator.Play("Laughing");
		this.m_firstFramePass = false;
		// TODO: play sound effect
	}


	public bool update(float dt)
	{
		this.m_motionCamera.LookAt(this.m_drNastyGO.transform);
		if (!this.m_firstFramePass)
		{
			this.m_firstFramePass = true;
			return false;
		}

		if (this.m_drNastyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
		{
			Debug.Log("done!");
			return true;
		}

		return false;
	}

	public GameCamera gameCamera { get { return m_camera; }
		set
		{
			this.m_camera = value;
		}
	}

	public GameObject nsBossGO { get {  return m_nsBossGO; } 
	set { m_nsBossGO = value; }
	}
}
