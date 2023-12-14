using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] float amount = 10f;
    [SerializeField] float radius = 10f;
    [SerializeField] float speed = 10f;

    [SerializeField] private Buff buff;

    GameObject player;
    public LayerMask PlayerLayer; // player's layer name

    bool playerInRange;

    private void Awake()
    {
        //player = GameManager.Instance.player;
        //GetComponent<TargetMovement>().SetTarget(player.transform);

    }

    private void Start()
    {
        player = GameManager.Instance.player;
        GetComponent<TargetMovement>().SetTarget(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is near enough
        //playerInRange = Physics.CheckSphere(transform.position, radius, PlayerLayer);

        //if (playerInRange)
        //{
        //    Move();
        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //TODO:add exp to player
            buff.ApplyTo(other.gameObject);
            Debug.Log("Add Exp");
            Destroy(gameObject);
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
