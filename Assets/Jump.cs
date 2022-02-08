using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = false;
        Player.Instance.jump = false;
        Player.Instance.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        if(Player.Instance.acceleration){
Player.Instance.gameObject.transform.position = new Vector3(-2.5f,Player.Instance.y,Player.Instance.gameObject.transform.position.z);
        }
        else
        {
            Player.Instance.gameObject.transform.position = new Vector3(0,Player.Instance.y,Player.Instance.gameObject.transform.position.z);
        }
        
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
