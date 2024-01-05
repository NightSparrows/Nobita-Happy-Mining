using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Attack))]
public class Ghost2 : MonoBehaviour
{
    [SerializeField] private GameObject explosionOnDead;
    [SerializeField] private SoundEffectSO deadSound;
    [SerializeField] private SoundEffectSO getHitSound;
    [SerializeField] private GameObject expPickUp;

    //
    private static readonly int animIsMoving = Animator.StringToHash("is moving");   // bool
    private static readonly int animAttack = Animator.StringToHash("attack");        // trigger
    private static readonly int animGetHit = Animator.StringToHash("get hit");       // trigger
    private static readonly int animIsAlive = Animator.StringToHash("is alive");     // bool
    private static readonly int animAttack2 = Animator.StringToHash("attack2");        // trigger

    //
    private static readonly int animMoveState = Animator.StringToHash("fly");
    private static readonly int animAttackState = Animator.StringToHash("attack");
    private static readonly int animGetHitState = Animator.StringToHash("hit");
    private static readonly int animDieState = Animator.StringToHash("playing");

    [SerializeField] private Animator anim;
    private NavigationMove movement;
    private Health health;
    private Attack attack;

    //
    [SerializeField] GameObject bullet;
    [SerializeField] RangeDetector rangeDetector;
    private NavMeshAgent agent;
    private GameObject player;
    [SerializeField] LayerMask obstructionMask;
    private bool toAttack = false;
    private float cooldownTime = 5f;
    private bool notCooldown = true;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        //var player = GameManager.Instance.player;
        player = GameManager.Instance.player;
        movement = GetComponent<NavigationMove>();
        movement.SetTarget(player.transform);
        attack = GetComponent<Attack>();

        AnimationEndHandler[] animHandlers = anim.GetBehaviours<AnimationEndHandler>();
        foreach (var handler in animHandlers)
        {
            handler.OnAnimationEnd += OnAnimationEnd;
        }

        health = GetComponent<Health>();
        health.OnDead += OnDead;
        health.OnHealthChanged += OnHealthChanged;

        // far attack
        rangeDetector.OnRangeStay += CheckToAttack;
        rangeDetector.OnRangeExit += Chase;
        agent = GetComponent<NavMeshAgent>();

        anim.SetBool(animIsMoving, movement.target != null);
    }

    private void OnDead()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        anim.SetBool(animIsAlive, false);
        movement.enableMove = false;
        movement.baseValue = 0f;
        Instantiate(explosionOnDead, transform.position, Quaternion.identity);
        deadSound.Play();
    }

    private void OnGetHit()
    {
        anim.SetTrigger(animGetHit);
        movement.enableMove = false;
        getHitSound.Play();
    }

    private void OnHealthChanged(int orgValue, int newValue)
    {
        if (health.isDead()) return;

        // get hit
        OnGetHit();
    }

    void OnAttack()
    {
        anim.SetTrigger(animAttack);
        movement.enableMove = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("ghost hit " + collision.gameObject.name);
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("ghost1 hit " + collision.gameObject.name);
        //    Health playerHealth = collision.gameObject.GetComponent<Health>();
        //    playerHealth.takeDamage(attack.Damage);

        //    OnAttack();
        //}
    }

    private void Update()
    {
        //anim.SetBool(animIsMoving, movement.target != null);

        if (toAttack)
        {
            anim.SetBool(animIsMoving, false);
            agent.speed = 0;
            FaceTarget(player.transform.position);
            if (notCooldown)
            {
                notCooldown = false;
                anim.SetTrigger(animAttack2);
                Debug.Log("attack2");
                ShootAttack();
            }
        }
        else
        {
            agent.speed = 3.5f;
            anim.SetBool(animIsMoving, true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnAttack();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnGetHit();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnDead();
        }
    }

    private void OnAnimationEnd(AnimatorStateInfo info, int layerIndex)
    {
        int state = info.shortNameHash;
        if (state == animAttackState)
        {
            if (anim.GetBool(animIsAlive))
            {
                Vector3 p = player.transform.position;
                p.y = 0;
                // shoot bullet
                GameObject mBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                mBullet.transform.LookAt(p);
            }
        }

        if (state == animGetHitState)
        {
            if (anim.GetBool(animIsAlive))
                movement.enableMove = true;
        }

        if (state == animDieState)
        {
            Instantiate(expPickUp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //
    private void CheckToAttack(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = player.transform.position;

            Vector3 dir = (p2 - p1).normalized;

            RaycastHit hit;
            bool hasHitObstacle = Physics.Raycast(p1, dir, out hit, Vector3.Distance(p1, p2), obstructionMask);

            if (hasHitObstacle)
            {
                toAttack = false;
                //agent.speed = movement.speed;
                //anim.SetBool(animIsMoving, true);
                //Debug.Log("ray hit obstacle");
            }
            else if(toAttack == false)
            {
                toAttack = true;
                //agent.speed = 0;
                //anim.SetBool(animIsMoving, false);
                Debug.Log("start attack2");
            }
        }

    }

    private void ShootAttack()
    {
        //GameObject mBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Invoke("Cooldown", cooldownTime);
    }

    private void Chase(Collider other)
    {
        toAttack = false;
        //agent.speed = movement.speed;
        //anim.SetBool(animIsMoving, true);
        Debug.Log("start chase");
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
    private void Cooldown()
    {
        notCooldown = true;
    }
}
