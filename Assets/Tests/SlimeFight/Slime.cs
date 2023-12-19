using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Attack))]
public class Slime : MonoBehaviour
{
    [SerializeField] private GameObject explosionOnDead;
    [SerializeField] private SoundEffectSO deadSound;
    [SerializeField] private SoundEffectSO getHitSound;

    private Animator anim;
    private TargetMovement movement;
    private Health health;
    private Attack attack;

    private static readonly int animIsMoving = Animator.StringToHash("is moving");
    private static readonly int animAttack = Animator.StringToHash("attack");
    private static readonly int animGetHit = Animator.StringToHash("get hit");
    private static readonly int animIsAlive = Animator.StringToHash("is alive");

    private static readonly int animMoveState = Animator.StringToHash("WalkFWD");
    private static readonly int animAttackState = Animator.StringToHash("Attack01");
    private static readonly int animGetHitState = Animator.StringToHash("GetHit");
    private static readonly int animDieState = Animator.StringToHash("Die");

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<TargetMovement>();
        attack = GetComponent<Attack>();

        AnimationEndHandler[] animHandlers = anim.GetBehaviours<AnimationEndHandler>();
        foreach (var handler in animHandlers)
        {
            handler.OnAnimationEnd += OnAnimationEnd;
        }

        health = GetComponent<Health>();
        health.OnDead += OnDead;
        health.OnHealthChanged += OnHealthChanged;

        if (movement.target == null)
        {
            movement.target = GameManager.Instance.player.transform;
        }
    }

    void OnDead()
    {
        GetComponent<SphereCollider>().enabled = false;
        anim.SetBool(animIsAlive, false);
        movement.enableMove = false;
        Instantiate(explosionOnDead, transform.position, Quaternion.identity);
        deadSound.Play();
    }

    void OnGetHit()
    {
        anim.SetTrigger(animGetHit);
        movement.enableMove = false;
        getHitSound.Play();
    }

    void OnHealthChanged(int newValue)
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
        //Debug.Log("slime hit " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
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
            Destroy(gameObject); 
        }
    }
}
