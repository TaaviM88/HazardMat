using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_Walk : StateMachineBehaviour
{
    public float speed = 2.5f;

    Vector3 player;
    Rigidbody2D rb;
    EnemyPathfinding pathfind;
    EnemyAI AI;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        pathfind = animator.GetComponent<EnemyPathfinding>();
        AI = animator.GetComponent<EnemyAI>();
        player = pathfind.GetTargetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        float reachedPositionDistance = 1f;
      
        if (AI.GetState() == EnemyAI.State.ChaseTarget)
        {
            if (pathfind.LookAtPlayer())
            {
                Vector2 target = new Vector2(pathfind.GetTargetTransform().x, rb.position.y); //pathfind.GetTargetTransform().position; //new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                CheckFacingDirection(animator, newPos);
                if (Vector3.Distance(animator.transform.position, target) < reachedPositionDistance)
                {
                    AI.SetState(EnemyAI.State.Attacking);
                    return;
                }
                else                
                    rb.MovePosition(newPos);
            }
        }
     
       else
        {
            Vector2 target = new Vector2(pathfind.GetTargetTransform().x, rb.position.y); //pathfind.GetTargetTransform().position; //new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            CheckFacingDirection(animator, newPos);
            rb.MovePosition(newPos);
            //Debug.Log(Vector3.Distance(animator.transform.position, target));
            /* if (Vector3.Distance(animator.transform.position, target) < reachedPositionDistance)
             {
                 Debug.Log("Lul");
                 //AI.SetState(EnemyAI.State.Attacking);
                 return;
             }
             else
             {

             }*/
        }
        
    }

    private void CheckFacingDirection(Animator animator, Vector2 newPos)
    {
        Vector2 dir = (newPos - (Vector2)animator.transform.position);
        if (dir.x < 0 && animator.transform.localScale.x != -1)
        {
            pathfind.FlipEnemy();
        }

        if (dir.x > 0 && animator.transform.localScale.x != 1)
        {
            pathfind.FlipEnemy();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
