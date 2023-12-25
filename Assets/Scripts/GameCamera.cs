using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public enum ViewType
	{
		Immediate,
		Smooth
	}

	public float followSpeed = 2.0f;
	public Transform m_currentTarget;
    public ViewType m_viewType;

    // Start is called before the first frame update
    void Start()
    {
        this.m_viewType = ViewType.Immediate;
    }

    public void setTarget(Transform transform)
    {
        this.m_currentTarget = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_currentTarget == null)
            return;
        switch (this.m_viewType)
        {
            case ViewType.Immediate:
                transform.position = this.m_currentTarget.position;
                this.transform.rotation = this.m_currentTarget.rotation;
                break;
            case ViewType.Smooth:
				transform.position = Vector3.Slerp(transform.position, this.m_currentTarget.position, followSpeed * Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, this.m_currentTarget.rotation, followSpeed * Time.deltaTime);
				break;
            default:
                break;
        }
    }
}
