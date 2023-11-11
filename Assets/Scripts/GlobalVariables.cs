using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GameObject player;

    public GameObject m_player;

	private void Awake()
	{
		player = this.m_player;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
