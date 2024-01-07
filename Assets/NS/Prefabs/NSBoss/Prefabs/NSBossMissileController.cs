using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSBossMissileController : MonoBehaviour
{
    [SerializeField] private GameObject m_redSpot;

    public Player m_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.m_redSpot.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, 0.1f, this.transform.position.z), Quaternion.identity);
        this.transform.position -= new Vector3(0, 10f * Time.deltaTime, 0);
        if (this.transform.position.y <= 0)
        {
            float distance = Vector3.Distance(this.transform.position, this.m_player.transform.position);
			// Explosion when hit the ground
			if (distance <= 2f)
            {
                Health health = this.m_player.GetComponent<Health>();
                health.takeDamage((int)((float)health.MaxHealth * 0.1f));
            }
            Destroy(this.gameObject);
        }
    }

    public Player player { get { return m_player; } set { this.m_player = value; } }
}
