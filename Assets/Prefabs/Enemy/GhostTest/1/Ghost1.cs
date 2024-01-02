using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Attack))]
public class Ghost1 : MonoBehaviour
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
    private NavigationMove movement;
    private Health health;
    private Attack attack;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        var player = GameManager.Instance.player;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ghost1 hit " + collision.gameObject.name);
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.takeDamage(attack.Damage);

            OnAttack();
        }
    }

    private void Update()
    {
        anim.SetBool(animIsMoving, movement.target != null);

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
}
