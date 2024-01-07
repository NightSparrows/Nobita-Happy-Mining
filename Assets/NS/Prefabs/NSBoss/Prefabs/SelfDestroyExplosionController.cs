using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class SelfDestroyExplosionController : MonoBehaviour
{
    private float m_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Awake()
	{
        this.m_time = 0;
        this.GetComponent<AudioSource>().Play();
	}

	// Update is called once per frame
	void Update()
    {
        this.m_time += Time.deltaTime;

        if (this.m_time >= 5f)
        {
            Destroy(this.gameObject);
        }
    }
}
