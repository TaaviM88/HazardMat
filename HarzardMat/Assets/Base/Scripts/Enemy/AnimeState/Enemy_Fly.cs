using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : StateMachineBehaviour
{
    public float speed = 3f;
    Rigidbody2D rb2d;
    EnemyPathfinding pathfind;
    EnemyAI AI;
    EnemyManager emanager;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d = animator.GetComponent<Rigidbody2D>();
        pathfind = animator.GetComponent<EnemyPathfinding>();
        AI = animator.GetComponent<EnemyAI>();
        AI.SetState(EnemyAIState.flyforward);
        emanager = animator.GetComponent<EnemyManager>();
        //player = pathfind.GetTargetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(AI.GetState() ==EnemyAIState.flyforward)
         {
            rb2d.velocity = (emanager.side * Vector2.right * speed);
         }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d.velocity = Vector2.zero;    
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
