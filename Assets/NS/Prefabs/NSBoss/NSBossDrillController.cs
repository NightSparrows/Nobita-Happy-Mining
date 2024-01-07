using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSBossDrillController : MonoBehaviour
{
    private float m_damagePercentage = 0.3f;

    [SerializeField] private bool m_enableDamage = false;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
        {
            if (this.m_enableDamage)
            {
                Health health = other.GetComponent<Health>();
                health.takeDamage((int)((float)health.MaxHealth * this.m_damagePercentage));
            }
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool enableDamage {  get { return m_enableDamage; } set { m_enableDamage = value; } }
}
