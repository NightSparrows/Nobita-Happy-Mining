using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject player;
    //GameObject bullet;
    [SerializeField] GameObject exp = null;

    private Rigidbody rig;

    //[SerializeField] float speed = 3f;
    Health health;
    Attack attack;
    Defense defense;

    private void Awake()
	{
		rig = GetComponent<Rigidbody>();

        //player = GameObject.Find("Player");

        health = GetComponent<Health>();
        attack = GetComponent<Attack>();
        defense = GetComponent<Defense>();
        //SelfDestroy();

        //player = GameManager.Instance.player;
       // GetComponent<NavigationMove>().SetTarget(player.transform);

    }

    private void Start()
    {
        player = GameManager.Instance.player;


        health.OnDead += HandleDeath;
        //GetComponent<NavigationMove>().SetTarget(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyMove1();
    }

    //void EnemyMove1()
    //{
    //    Vector3 p1 = transform.position;
    //    Vector3 p2 = player.transform.position;
    //    Vector3 v = p2 - p1;

    //    v.y = 0;
    //    v = v.normalized;
    //    v *= speed * Time.deltaTime;

    //    transform.Translate(v);
    //    //rig.velocity = v;
    //}

    // private void OnTriggerEnter(Collider collision)
    // {
    //     // hit by bullet
    //     if (collision.tag == "PlayerBullet")
    //     {
    //BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
    //         this.health.takeDamage(bulletController.getDamage());

    //         Attack damage = collision.gameObject.GetComponent<Attack>();
    //         this.health.takeDamage(damage.Damage);


    //         //hp -= 25f;
    //         //Debug.Log("collide");

    //         //if (hp <= 0)
    //         if (this.health.isDead())
    //         {
    //             GameObject e = Instantiate(exp, transform.position, Quaternion.identity);
    //             e.GetComponent<Exp>().SetPlayer(player);
    //             e.transform.Translate(0f, -2.5f, 0f);
    //             Destroy(gameObject);
    //         }

    //     }


    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enemy hit player");
            collision.gameObject.GetComponent<Defense>().BeAttacked(attack); 
        }

    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<Defense>().BeAttacked(attack);
    //    }
    //}

    void HandleDeath()
    {
        Debug.Log("Enemy.HandleDeath");
        CreateExp();
        SelfDestroy();
    }

    void CreateExp()
    {
        GameObject e = Instantiate(exp, transform.position, Quaternion.identity);
        //e.GetComponent<Exp>().SetPlayer(player);
        //e.transform.Translate(0f, -2.5f, 0f);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        health.OnDead -= HandleDeath;
    }
}
