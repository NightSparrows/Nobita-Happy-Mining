using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] float amount = 10f;
    [SerializeField] float radius = 10f;
    [SerializeField] float speed = 10f;


    GameObject player;
    public LayerMask PlayerLayer; // player's layer name

    bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is near enough
        playerInRange = Physics.CheckSphere(transform.position, radius, PlayerLayer);

        if (playerInRange)
        {
            Move();
        }
    }

    // return amount of Exp
    public float getAmount()
    {
        return amount;
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }


    // go to player
    void Move()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = player.transform.position;
        Vector3 v = p2 - p1;

        v.y = 0;
        v = v.normalized;
        v *= speed * Time.deltaTime;

        transform.Translate(v);
    }
}
