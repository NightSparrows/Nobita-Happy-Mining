

using JetBrains.Annotations;
using UnityEngine;

public class SmoothFloat : MonoBehaviour
{
	private float m_target;
	private float m_currentValue;

	public SmoothFloat()
	{
		this.m_target = 0f;
		this.m_currentValue = 0f;
	}

	public SmoothFloat(float value)
	{
		this.m_target = value;
		this.m_currentValue = value;
	}

	public void setTarget(float target )
	{
		this.m_target = target;
	}

	private void Update()
	{
		
	}

	public static implicit operator float(SmoothFloat smoothFloat)
	{
		return smoothFloat.m_currentValue;
	}

}
