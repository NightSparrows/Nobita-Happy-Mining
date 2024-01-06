using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Attack))]
public class Ghost0 : MonoBehaviour
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

    //
    private static readonly int animMoveState = Animator.StringToHash("fly");
    private static readonly int animAttackState = Animator.StringToHash("attack");
    private static readonly int animGetHitState = Animator.StringToHash("hit");
    private static readonly int animDieState = Animator.StringToHash("playing");

    [SerializeField] private Animator anim;
    private ForwardMovement movement;
    private Health health;
    private Attack attack;

    //----------------------------------------------------
    [SerializeField] GameObject attackArea;
    private GameObject player;
    Vector3 dir;
    //----------------------------------------------------

    private void Start()
    {
        //anim = GetComponent<Animator>();
        player = GameManager.Instance.player;
        movement = GetComponent<ForwardMovement>();
        attack = GetComponent<Attack>();

        AnimationEndHandler[] animHandlers = anim.GetBehaviours<AnimationEndHandler>();
        foreach (var handler in animHandlers)
        {
            handler.OnAnimationEnd += OnAnimationEnd;
        }

        health = GetComponent<Health>();
        health.OnDead += OnDead;
        health.OnHealthChanged += OnHealthChanged;

        //------------------------------------------
        SetDirection();
        StartCoroutine(DashAttack());
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
        //anim.SetTrigger(animGetHit);
        //movement.enableMove = false;
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
        //anim.SetTrigger(animAttack);
        //movement.enableMove = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ghost hit " + collision.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ghost0 hit " + other.gameObject.name);
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.takeDamage(attack.Damage);

            OnAttack();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log("ghost hit " + collision.gameObject.name);
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("ghost0 hit " + collision.gameObject.name);
    //        Health playerHealth = collision.gameObject.GetComponent<Health>();
    //        playerHealth.takeDamage(attack.Damage);

    //        OnAttack();
    //    }
    //}

    private void Update()
    {
        //anim.SetBool(animIsMoving, movement.target != null);

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    OnAttack();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    OnGetHit();
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    OnDead();
        //}
    }

    private void OnAnimationEnd(AnimatorStateInfo info, int layerIndex)
    {
        int state = info.shortNameHash;
        if (state == animAttackState || state == animGetHitState)
        {
            if (anim.GetBool(animIsAlive))
                movement.enableMove = true;
        }
        else if (state == animDieState)
        {
            Instantiate(expPickUp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }


    //---------------------------------------------------------------------
    void SetDirection()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = player.transform.position;

        dir = (p2 - p1).normalized;
    }

    IEnumerator DashAttack()
    {
        //yield return new WaitForSeconds(1f);
        GameObject area = Instantiate(attackArea, transform.position, Quaternion.identity);
        area.transform.Translate(0, -transform.position.y, 0);
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        pos.y = 0;
        area.transform.LookAt(pos);
        area.transform.localScale += new Vector3(0, 0, 3);
        yield return new WaitForSeconds(1.3f);
        Destroy(area);
        // TODO:MOVE
        GetComponent<ForwardMovement>().enabled = true;
        anim.SetBool(animIsMoving, true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
