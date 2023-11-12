using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public float followSpeed = 2.0f;
	public Transform m_currentTarget;

    private Vector3 offset = new Vector3(10f, -20f, -20f);

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
			Vector3 newPos = this.m_currentTarget.position - offset;
			transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
            //Quaternion newRot = this.m_currentTarget.rotation;
            Quaternion org = transform.rotation;
            transform.LookAt(m_currentTarget);
            Quaternion after = transform.rotation;
            transform.rotation = Quaternion.Slerp(org, after, followSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRot, followSpeed * Time.deltaTime);
		}
    }
}
