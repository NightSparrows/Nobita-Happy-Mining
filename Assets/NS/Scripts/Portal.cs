using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal targetPortal;

    public ParticleSystem transferAwayPS;
    public ParticleSystem transferHerePS;

    public bool activate = true;
    private GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if (!this.activate)
            return;

        if (targetPortal == null)
        {
            Debug.LogError("You haven't set the target portal yet!");
        }

        Debug.Log("trigger Name:" + other.name);

        if (other.name == "Player")
        {
            Debug.Log("start teleporting...");
            this.targetObject = other.gameObject;
			StartCoroutine("teleport");
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player")
		{
			StartCoroutine("teleportCooldown");
		}
	}

    IEnumerator teleportCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        this.activate = true;
    }

	IEnumerator teleport()
    {
        this.transferAwayPS.Play();
        CharacterController characterController = this.targetObject.GetComponent<CharacterController>();
        characterController.enabled = false;
        this.targetPortal.activate = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("target portal position: " + this.targetPortal.transform.position);
		this.targetObject.transform.position = 
            new Vector3(this.targetPortal.transform.position.x, this.targetObject.transform.position.y, this.targetPortal.transform.position.z);
		this.targetPortal.transferHerePS.Play();
		yield return new WaitForSeconds(0.5f);
		characterController.enabled = true;
		Debug.Log("teleportation done");
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
