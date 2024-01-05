using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSLevelStartDirector
{
	public delegate bool MotionUpdateFunPtr(float dt);
	public delegate void MotionInitFunPtr();

	private GameCamera m_gameCamera;
    private GameObject m_nsBossGO;
    
    
    private int m_currentMotion;
	private List<IAnimationMotion> m_motions;

	/// <summary>
	/// Initialize the for
	/// </summary>
	public void initDirector()
    {
        // init
        this.m_currentMotion = 0;
        this.m_motions = new List<IAnimationMotion>();

        // add functions
        var laughMotion = new NSBossLaughMotion();
        laughMotion.gameCamera = this.m_gameCamera;
        laughMotion.nsBossGO = this.m_nsBossGO;
		this.m_motions.Add(laughMotion);

        this.m_motions[0].init();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeDelta"></param>
    /// <returns>
    /// If is true: mean the animation is done
    /// otherwise is false
    /// </returns>
    public bool update(float timeDelta)
    {
        if (this.m_motions[this.m_currentMotion].update(timeDelta))
        {
            this.m_currentMotion++;
            if (this.m_currentMotion == this.m_motions.Count)
            {
                Debug.Log("director is done!");
                return true;
            }
            this.m_motions[this.m_currentMotion].init();
		}
        return false;
    }

    // getter setter
    public GameCamera gameCamera
    {
        get { return this.m_gameCamera; }
        set
        {
            this.m_gameCamera = value;
        }
    }

    public GameObject nsBossGO
    {
        get { return this.m_nsBossGO; }
        set
        {
            this.m_nsBossGO = value;
        }
    }
}
