using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
	public GameObject portal1;
	public GameObject portal2;
	public GameObject portal3;
	public GameObject portal4;

	GameObject portalPrefab;

	// Start is called before the first frame update
	void Start()
	{
		portalPrefab = Resources.Load<GameObject>("Prefabs/PortalPrefab");

		this.portal1 = Instantiate(portalPrefab, new Vector3(10, 0, 10), Quaternion.identity);
		this.portal2 = Instantiate(portalPrefab, new Vector3(10, 0, -10), Quaternion.identity);
		this.portal3 = Instantiate(portalPrefab, new Vector3(-10, 0, -10), Quaternion.identity);
		this.portal4 = Instantiate(portalPrefab, new Vector3(-10, 0, 10), Quaternion.identity);

		Portal portal1Control = this.portal1.GetComponent<Portal>();
		Portal portal2Control = this.portal2.GetComponent<Portal>();
		Portal portal3Control = this.portal3.GetComponent<Portal>();
		Portal portal4Control = this.portal4.GetComponent<Portal>();
		portal1Control.targetPortal = portal2Control;
		portal2Control.targetPortal = portal1Control;
		portal3Control.targetPortal = portal4Control;
		portal4Control.targetPortal = portal3Control;

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
