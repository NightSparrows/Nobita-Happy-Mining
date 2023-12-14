using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private GameObject explosionOnDead;

    private Animator anim;
    private TargetMovement movement;

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

        AnimationEndHandler[] animHandlers = anim.GetBehaviours<AnimationEndHandler>();
        foreach (var handler in animHandlers)
        {
            handler.OnAnimationEnd += OnAnimationEnd;
        }
    }

    private void Update()
    {
        anim.SetBool(animIsMoving, movement.target != null);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger(animAttack);
            movement.enableMove = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger(animGetHit);
            movement.enableMove = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool(animIsAlive, false);
            movement.enableMove = false;
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
            Instantiate(explosionOnDead, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
}
