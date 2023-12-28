using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private Buff buff;

    GameObject player;

    private void Start()
    {
        player = GameManager.Instance.player;
        GetComponent<TargetMovement>().SetTarget(player.transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //TODO:add diamond to player
            buff.ApplyTo(other.gameObject);
            Debug.Log("Add Diamond");
            Destroy(gameObject);
        }
    }
}
