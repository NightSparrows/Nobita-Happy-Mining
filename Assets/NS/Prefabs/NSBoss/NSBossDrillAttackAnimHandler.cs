using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NSBossBehaviorScript;

public class NSBossDrillAttackAnimHandler : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var bossScript = animator.transform.parent.GetComponent<NSBossBehaviorScript>();

		float currentTime = stateInfo.normalizedTime % 1;
		if (currentTime >= 0.4 && currentTime <= 0.6)
		{
			Vector3 vector = bossScript.m_player.transform.position - bossScript.m_vehicleGO.transform.position;
			Quaternion targetRotation = Quaternion.LookRotation(vector, Vector3.up);
			bossScript.m_vehicleGO.transform.rotation = Quaternion.RotateTowards(bossScript.m_vehicleGO.transform.rotation, targetRotation, bossScript.rotateSpeed * Time.deltaTime);

			bossScript.m_drillAttackVector = bossScript.m_vehicleGO.transform.rotation * Vector3.forward;
            bossScript.m_drillAttackVector.Normalize();
			bossScript.m_drillAttackCurrentForwardSpeed += bossScript.moveSpeed * Time.deltaTime;
		}
		else if (currentTime > 0.6)
		{
			// TODO: enable attack modifier(Doing damage)
		}

		bossScript.move(bossScript.m_drillAttackCurrentForwardSpeed * bossScript.m_drillAttackVector * Time.deltaTime);
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var bossScript = animator.transform.parent.GetComponent<NSBossBehaviorScript>();


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
