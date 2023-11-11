using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    //public Transform player;

    GameObject player;
    GameObject bullet;
    [SerializeField] GameObject exp = null;

    private Rigidbody rig;

    [SerializeField] float speed = 3f;
    //[SerializeField] int hp = 100;
    Health health;
    public float atk = 10f;

	private void Awake()
	{
		rig = GetComponent<Rigidbody>();

		//player = GameObject.Find("Player");
		agent = GetComponent<NavMeshAgent>();

        health = GetComponent<Health>();
        //SelfDestroy();
	}

    // Update is called once per frame
    void Update()
    {
        EnemyMove1();
        //EnemyMove2();
    }

    void EnemyMove1()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = player.transform.position;
        Vector3 v = p2 - p1;
        
        v.y = 0;
        v = v.normalized;
        v *= speed * Time.deltaTime;

        transform.Translate(v);
        //rig.velocity = v;
    }

    void EnemyMove2()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // hit by bullet
        if (collision.tag == "PlayerBullet")
        {
			BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
            this.health.takeDamage(bulletController.getDamage());

            //hp -= 25f;
            //Debug.Log("collide");

            //if (hp <= 0)
            if (this.health.isDeath())
            {
                //GameObject e = Instantiate(exp, transform.position, Quaternion.identity);
                //e.GetComponent<Exp>().SetPlayer(player);
                Destroy(gameObject);
            }
                
        }

        //bullet = collision.gameObject;
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }

    void SelfDestroy()
    {
        Destroy(gameObject, 5);
    }

    private void OnDestroy()
    {
        GameObject e = Instantiate(exp, transform.position, Quaternion.identity);
        e.GetComponent<Exp>().SetPlayer(player);
        //Destroy(gameObject);
    }
}
