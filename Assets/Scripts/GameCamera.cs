using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public float followSpeed = 2.0f;
	public Transform m_currentTarget;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setTarget(Transform transform)
    {
        this.m_currentTarget = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_currentTarget != null)
		{
			transform.position = Vector3.Slerp(transform.position, this.m_currentTarget.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, this.m_currentTarget.rotation, followSpeed * Time.deltaTime);
		}
    }
}
