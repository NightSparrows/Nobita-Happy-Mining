using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// auto target

public class Bullet_1 : MonoBehaviour
{
    public Transform enemy;

    private Rigidbody rig;

    [SerializeField] float speed = 5f;
    [SerializeField] float atk = 25f;
    int level = 1;

    Attack attack;

    Vector3 enemyPos;

    Vector3 direction;

    private void Awake()
    {
        attack = GetComponent<Attack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();

        //enemy = GameObject.Find("Enemy").transform;
        //if (enemy == null)
        //{
        //    enemyPos = enemy.position;
        //    SetDirection();
        //}
        //else
        //{
        //    direction = new Vector3(1, 0, 0);
        //}

        rig.velocity = Vector3.forward * speed;

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        //Bullet1_Move();
    }

    void Bullet1_Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
    }

    void SetDirection()
    {
        Vector3 p1 = transform.position;
        //Vector3 p2 = enemy.position;
        Vector3 p2 = enemyPos;
        direction = p2 - p1;

        direction.y = 0;
        direction = direction.normalized;
    }

    public float getAtk()
    {
        return atk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("bullet hit enemy");
            other.gameObject.GetComponent<Defense>().BeAttacked(attack);
        }
    }

}
