using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : StateMachineBehaviour
{
    public float speed = 3f;
    [Header("Speed  of sine movement")]
    public float frequency = 20.0f;
    [Header("Size of sine movement")]
    public float magnitude = 0.5f;
    private Vector3 axis;
    private Vector3 pos;
    float sinIndex = 1;
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
        //pos = rb2d.position;
        pos = rb2d.transform.right;
        axis = rb2d.transform.up;
        //player = pathfind.GetTargetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(AI.GetState() ==EnemyAIState.flyforward)
         {       
            rb2d.velocity = new Vector2( - 1 * emanager.side * speed * pos.x  ,axis.y * Mathf.Sin(Time.time * frequency) * Time.deltaTime * magnitude);
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
