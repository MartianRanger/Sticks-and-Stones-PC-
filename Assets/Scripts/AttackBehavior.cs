using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    //This script is basically for handling anything the enemy might do while in its enemy state, but I honestly haven't done anything with it yet.

    public Transform speechBubbleSpawn;
    public GameObject speechBubble;
    private float rotationSpeed = 3.0f;
    public Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Shoot();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.transform.LookAt(player.transform.position);
    }

    /*
     * void UpdateTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.transform.position);
        gunEnd.LookAt(player.transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }
    */
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
