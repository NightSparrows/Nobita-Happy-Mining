

using JetBrains.Annotations;
using UnityEngine;

public class SmoothFloat
{
	private float m_target;
	private float m_currentValue;
	private float m_speed = 1f;

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

	public void setSpeed(float speed)
	{
		this.m_speed = speed;
	}

	public float getSpeed()
	{
		return this.m_speed;
	}

	public void setTarget(float target)
	{
		this.m_target = target;
	}

	public float getTarget()
	{
		return this.m_target;
	}

	public void update(float deltaTime)
	{
		float delta = this.m_target - this.m_currentValue;
		delta = delta * deltaTime * this.m_speed;
		this.m_currentValue += delta;
	}

	public static implicit operator float(SmoothFloat smoothFloat)
	{
		return smoothFloat.m_currentValue;
	}

}
