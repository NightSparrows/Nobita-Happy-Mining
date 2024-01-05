using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //[SerializeField] private GameObject shootFX;
    //[SerializeField] private GameObject hitFX;

    Attack attack;

    private void Awake()
    {
        attack = GetComponent<Attack>();
        Destroy(gameObject, 10);
    }

    private void Start()
    {
        //GameObject fx = Instantiate(shootFX, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Bullet hit " + collision.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health == null)
            {
                Debug.LogWarning("Player " + other.gameObject.name + "'s Health not found");
                return;
            }

            //Instantiate(hitFX, transform.position, Quaternion.Inverse(transform.rotation));
            health.takeDamage(attack.Damage);
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Mineral"))
        {
            //Debug.Log("Bullet hit mineral");
            //Instantiate(hitFX, transform.position, Quaternion.Inverse(transform.rotation));
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Bullet hit unknown object: " + other.gameObject.name);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log("Bullet hit " + collision.gameObject.name);
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Health health = collision.gameObject.GetComponent<Health>();
    //        if (health == null)
    //        {
    //            Debug.LogWarning("Player " + collision.gameObject.name + "'s Health not found");
    //            return;
    //        }

    //        //Instantiate(hitFX, transform.position, Quaternion.Inverse(transform.rotation));
    //        health.takeDamage(attack.Damage);
    //        Destroy(gameObject);
    //    }

    //    else if (collision.gameObject.CompareTag("Mineral"))
    //    {
    //        //Debug.Log("Bullet hit mineral");
    //        //Instantiate(hitFX, transform.position, Quaternion.Inverse(transform.rotation));
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("Bullet hit unknown object: " + collision.gameObject.name);
    //    }
    //}
}
