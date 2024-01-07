using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSLevelStartDirector
{

    private int m_currentMotion;
	private List<IAnimationMotion> m_motions;

    public NSLevelStartDirector()
	{
		// init
		this.m_motions = new List<IAnimationMotion>();
	}

    public void addMotion(IAnimationMotion motion)
    {
        this.m_motions.Add(motion);
    }

	/// <summary>
	/// Initialize the for
	/// </summary>
	public void initDirector()
	{
		this.m_currentMotion = 0;
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
}
