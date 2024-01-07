using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NSBossBehaviorScript;

public class NSBossBarrelAttackAnimHandler : StateMachineBehaviour
{
    [SerializeField] private GameObject m_explosionFXPrefab;
    [SerializeField] private AudioClip m_explosionClip;
    [SerializeField] private GameObject m_missilePrefab;


    private bool m_triggerExplsionFX;
    private GameObject m_explosionFX;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.m_triggerExplsionFX = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var bossScript = animator.transform.parent.GetComponent<NSBossBehaviorScript>();

		if (stateInfo.normalizedTime >= 0.52 && !this.m_triggerExplsionFX)
		{

			this.m_explosionFX = GameObject.Instantiate(this.m_explosionFXPrefab, bossScript.transform.position, bossScript.transform.rotation);
			this.m_explosionFX.transform.Translate(new Vector3(-2.5f, 4f, 0));
            var audioSource = animator.GetComponent<AudioSource>();
            audioSource.clip = this.m_explosionClip;
            audioSource.Play();

            // because the missile is self destroy just instantiate it
            var missileGO = GameObject.Instantiate(this.m_missilePrefab, bossScript.m_player.transform.position + new Vector3(0, 40f, 0), Quaternion.identity);
            missileGO.GetComponent<NSBossMissileController>().player = bossScript.m_player;

            for(int i = 0; i < 4; i++)
			{
				float offsetX = Random.Range(-10, 10);
				float offsetZ = Random.Range(-10, 10);
				missileGO = GameObject.Instantiate(this.m_missilePrefab, bossScript.m_player.transform.position + new Vector3(offsetX, 40f, offsetZ), Quaternion.identity);
				missileGO.GetComponent<NSBossMissileController>().player = bossScript.m_player;
			}


			this.m_triggerExplsionFX = true;
		}
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		var bossScript = animator.transform.parent.GetComponent<NSBossBehaviorScript>();

        if (this.m_explosionFX != null)
        {
            Destroy(this.m_explosionFX);
        }

		bossScript.changeBossState(BossState.Idle);
	}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
